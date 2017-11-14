using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Framework;

namespace WebForms.MVVM.Leitores
{
	public class LeitorRegistros
	{

		private LeitorEnum _leitorEnum;

		public LeitorRegistros()
		{
			_leitorEnum = new LeitorEnum(null);
		}

		public TCampos Ler<TCampos>(DataRow fonteDeDados) where TCampos : new()
		{
			TCampos camposDaTela = new TCampos();
			Ler(camposDaTela, fonteDeDados);
			return camposDaTela;
		}

		private void Ler(object objeto, DataRow fonteDeDados)
		{
			var tipoDoObjeto = objeto.GetType();
			foreach (var propriedade in tipoDoObjeto.GetProperties())
			{
				ComponenteAttribute componente = Consultador.ConsultarConfiguracaoDaPropriedade(propriedade);
				if ((componente != null) && (FonteDeDadosPossuiCampo(fonteDeDados, componente)))
				{
					if (Comparador.EhUmEnum(propriedade.PropertyType))
					{
						_leitorEnum.PreencherPropriedadeEnum(objeto, propriedade, componente, fonteDeDados);
					}
					else if (!Comparador.EhUmaListaGenerica(propriedade.PropertyType))
					{
						PreencherPropriedade(objeto, propriedade, componente, fonteDeDados);
					}
				}
			}
		}

		private bool FonteDeDadosPossuiCampo(DataRow fonteDeDados, ComponenteAttribute componente)
		{
			return (!(
				(fonteDeDados.Table == null) ||
				(string.IsNullOrEmpty(componente.CampoDados)) ||
				(!fonteDeDados.Table.Columns.Contains(componente.CampoDados))
			));
		}

		private void PreencherPropriedade(object objeto, PropertyInfo propriedade, ComponenteAttribute componente, DataRow fonteDeDados)
		{
			object valorDaPropriedade = null;
			var valorDoCampo = fonteDeDados[componente.CampoDados];

			try
			{
				if (valorDoCampo == DBNull.Value)
					valorDaPropriedade = null;
				else
				{
					if (Comparador.EhCampoNullable(propriedade.PropertyType))
						valorDaPropriedade = Convert.ChangeType(valorDoCampo, Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedade));
					else
						valorDaPropriedade = Convert.ChangeType(valorDoCampo, propriedade.PropertyType);
				}
			}
			catch
			{
			}

			propriedade.SetValue(objeto, valorDaPropriedade, null);
		}

		public void Ler<TCampos>(IList objetos, DataTable registros) where TCampos : new()
		{
			foreach (DataRow registro in registros.Rows)
				objetos.Add(Ler<TCampos>(registro));
		}

	}
}
