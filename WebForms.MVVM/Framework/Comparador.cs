using System;
using System.Collections.Generic;
using System.Web.UI;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM.Framework
{
	public class Comparador
	{

		public static bool EhControleDeEdicao(Type tipo)
		{
			return (!EhComponenteTitulo(tipo)) 
				&& (typeof(Control).IsAssignableFrom(tipo));
		}

		public static bool EhUmaListaGenerica(Type tipo)
		{
			if (!tipo.IsGenericType)
				return false;
			var definicao = tipo.GetGenericTypeDefinition();
			return (definicao == typeof(IList<>)) 
				|| (definicao == typeof(List<>));
		}

		public static bool EhComponenteTitulo(Type tipo)
		{
			return typeof(IControleTitulo).IsAssignableFrom(tipo);
		}

		public static bool EhTipoBoolean(Type tipo)
		{
			return tipo.Equals(typeof(bool));
		}

		public static bool EhUmEnum(Type tipo)
		{
			return tipo.IsEnum;
		}

		public static bool EhCampoNullable(Type tipo)
		{
			if (!tipo.IsGenericType)
				return false;
			var definicao = tipo.GetGenericTypeDefinition();
			return (definicao == typeof(Nullable<>));
		}

	}
}
