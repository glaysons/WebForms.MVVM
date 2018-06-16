using System;
using System.Linq.Expressions;
using System.Reflection;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Framework;
using WebForms.MVVM.Leitores;

namespace WebForms.MVVM
{
	public class LeitorTela
	{

		private DicionarioTela _dicionario;
		private LeitorGrade _leitorGrade;
		private LeitorEnum _leitorEnum;

		public LeitorTela(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
			if (_dicionario == null)
				throw new Exception("Favor informar um dicionario válido!");
			_leitorGrade = new LeitorGrade(_dicionario);
			_leitorEnum = new LeitorEnum(_dicionario);
		}

		public TCamposDaTela Ler<TCamposDaTela>() where TCamposDaTela : new()
		{
			TCamposDaTela camposDaTela = new TCamposDaTela();
			Ler(camposDaTela);
			return camposDaTela;
		}

		public object Ler<TCamposDaTela>(Expression<Func<TCamposDaTela, object>> raiz)
		{
			var caminho = ExpressionHelper.CamihoDaExpressao(raiz);

			var propriedade = typeof(TCamposDaTela).GetProperty(caminho);
			if ((propriedade == null) || (!Comparador.EhUmaListaGenerica(propriedade.PropertyType)))
				throw new Exceptions.PropriedadeInvalidaException(caminho);

			_dicionario.AtivarCaminhoRaiz(caminho);
			try
			{
				var tipo = propriedade.PropertyType.GetGenericArguments()[0];
				var camposDaTela = Activator.CreateInstance(tipo);
				Ler(camposDaTela);
				return camposDaTela;
			}
			finally
			{
				_dicionario.DesativarCaminhoRaiz();
			}
		}

		private void Ler(object objeto)
		{
			var tipoDoObjeto = objeto.GetType();
			foreach (var propriedade in tipoDoObjeto.GetProperties())
			{
				var componente = Consultador.ConsultarConfiguracaoDaPropriedade(propriedade);
				if (componente != null)
				{
					if (Comparador.EhUmaListaGenerica(propriedade.PropertyType))
						_leitorGrade.PreencherPropriedadeLista(objeto, propriedade, componente);

					else if (Comparador.EhUmEnum(propriedade.PropertyType))
						_leitorEnum.PreencherPropriedadeEnum(objeto, propriedade, componente);

					else
						PreencherPropriedade(objeto, propriedade, componente);
				}
			}
		}

		private void PreencherPropriedade(object objeto, PropertyInfo propriedade, ComponenteAttribute componente)
		{
			object valorDaPropriedade = null;

			try
			{
				var valor = LeitorObjetos.Ler(propriedade, _dicionario, componente);
				if ((valor == null) || ((valor.GetType() == typeof(string)) && (string.IsNullOrEmpty((string)valor))))
					valorDaPropriedade = null;

				else if (Comparador.EhCampoNullable(propriedade.PropertyType))
					valorDaPropriedade = Convert.ChangeType(valor, Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedade));

				else
					valorDaPropriedade = Convert.ChangeType(valor, propriedade.PropertyType);
			}
			catch
			{
			}

			propriedade.SetValue(objeto, valorDaPropriedade, null);
		}

	}
}
