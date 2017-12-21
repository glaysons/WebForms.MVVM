using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Leitores;

namespace WebForms.MVVM.Atualizadores
{
	public class AtualizadorEnum
	{

		private DicionarioTela _dicionario;
		private LeitorEnum _leitorEnum;

		public AtualizadorEnum(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
			_leitorEnum = new LeitorEnum(_dicionario);
		}

		public void PreencherPropriedadeEnum(object objeto, PropertyInfo propriedade, ComponenteAttribute componente)
		{
			var controle = _dicionario.ConsultarComponenteEditor(propriedade, componente);
			if (controle == null)
				return;
			object valorDaPropriedade = null;
			if (objeto != null)
			{
				var opcao = ConsultarOpcaoDoObjeto(objeto, propriedade);
				valorDaPropriedade = _leitorEnum.ConsultarOpcaoComoTexto(propriedade.PropertyType, opcao);
			}
			AtualizadorObjetos.Atualizar(controle, valorDaPropriedade);
		}

		private Enum ConsultarOpcaoDoObjeto(object objeto, PropertyInfo propriedade)
		{
			return (Enum)propriedade.GetValue(objeto, null);
		}

	}
}
