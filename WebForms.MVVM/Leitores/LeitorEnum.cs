using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using WebForms.MVVM.Attributes;

namespace WebForms.MVVM.Leitores
{
	public class LeitorEnum
	{

		private DicionarioTela _dicionario;

		public LeitorEnum(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
		}

		public void PreencherPropriedadeEnum(object objeto, PropertyInfo propriedade, ComponenteAttribute componente)
		{
			if (_dicionario == null)
				throw new Exception("Não foi possível localizar um dicionario para consulta dos componentes!");

			var tipoEnum = propriedade.PropertyType;

			foreach (Enum opcao in tipoEnum.GetEnumValues())
				if (ExisteUmControleComEstaOpcao(componente, propriedade, tipoEnum, opcao))
				{
					propriedade.SetValue(objeto, opcao, null);
					return;
				}
		}

		private bool ExisteUmControleComEstaOpcao(ComponenteAttribute componente, PropertyInfo propriedade, Type tipoEnum, Enum opcao)
		{
			var valorTextoDaOpcao = ConsultarOpcaoComoTexto(tipoEnum, opcao);
			var controle = _dicionario.ConsultarComponenteEditor(propriedade, componente);

			if (controle != null)
			{
				try
				{
					return string.Equals(valorTextoDaOpcao, Convert.ToString(LeitorObjetos.Ler(controle)));
				}
				catch
				{
					return false;
				}
			}

			return false;
		}

		public string ConsultarOpcaoComoTexto(Type tipoEnum, Enum opcao)
		{
			var info = tipoEnum.GetField(opcao.ToString());
			if (info == null)
				return opcao.GetHashCode().ToString();

			var atributo = (DefaultValueAttribute)info.GetCustomAttributes(typeof(DefaultValueAttribute), inherit: false)
				.FirstOrDefault();

			if (atributo == null)
				return opcao.GetHashCode().ToString();

			try
			{
				return Convert.ToString(atributo.Value);
			}
			catch
			{
				return string.Empty;
			}
		}

		public void PreencherPropriedadeEnum(object objeto, PropertyInfo propriedade, ComponenteAttribute componente, DataRow fonteDeDados)
		{
			var tipoEnum = propriedade.PropertyType;
			foreach (Enum opcao in tipoEnum.GetEnumValues())
			{
				if (ExisteUmCampoComEstaOpcao(fonteDeDados, componente, tipoEnum, opcao))
				{
					try
					{
						propriedade.SetValue(objeto, opcao, null);
					}
					catch
					{
					}
					return;
				}
			}
		}

		private bool ExisteUmCampoComEstaOpcao(DataRow fonteDeDados, ComponenteAttribute componente, Type tipoEnum, Enum opcao)
		{
			var valorTextoDaOpcao = ConsultarOpcaoComoTexto(tipoEnum, opcao);
			var valorDoCampo = fonteDeDados[componente.CampoDados];
			if (valorDoCampo != DBNull.Value)
				try
				{
					return string.Equals(valorTextoDaOpcao, Convert.ToString(valorDoCampo));
				}
				catch
				{
				}
			return false;
		}

	}
}
