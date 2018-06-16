using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI.WebControls;
using WebForms.MVVM.Attributes;

namespace WebForms.MVVM.Framework
{
	public class Consultador
	{

		public static Type ConsultarTipoEspecificoDaPropriedadeGenerica(PropertyInfo propriedade)
		{
			var tipo = propriedade.PropertyType;
			if (tipo.IsGenericType)
				return tipo.GetGenericArguments().First();
			return null;
		}

		public static IList<ComponenteAttribute> ConsultarConfiguracaoDasPropriedades(PropertyInfo[] propriedadesDoItemDaLista)
		{
			var listaComponentes = new List<ComponenteAttribute>();
			foreach (PropertyInfo propriedade in propriedadesDoItemDaLista)
				listaComponentes.Add(ConsultarConfiguracaoDaPropriedade(propriedade));
			return listaComponentes;
		}

		public static ComponenteAttribute ConsultarConfiguracaoDaPropriedade(PropertyInfo propriedade)
		{
			foreach (var atributo in propriedade.GetCustomAttributes(typeof(ComponenteAttribute), inherit: true))
			{
				var componente = (ComponenteAttribute)atributo;
				if (string.IsNullOrEmpty(componente.CampoDados))
					componente.CampoDados = propriedade.Name;
				return componente;
			}
			return null;
		}

		public static int ConsultarIndiceDaBoundColumnDaGrade(DataGrid grade, string campoGrade)
		{
			for (var indice = 0; indice <= grade.Columns.Count - 1; indice++)
			{
				var coluna = grade.Columns[indice];
				if ((coluna is BoundColumn) && (string.Equals(((BoundColumn)coluna).DataField, campoGrade)))
					return indice;
			}
			return -1;
		}

		public static PropertyInfo ConsultarConfiguracaoDaExpressao(LambdaExpression campo)
		{
			var propriedade = campo.Body as MemberExpression;
			if (propriedade != null)
				return (PropertyInfo)propriedade.Member;

			var corpo = campo.Body as UnaryExpression;
			if (corpo == null)
				throw new Exception("Expressão inválida para captura da propriedade!");

			return (PropertyInfo)((MemberExpression)corpo.Operand).Member;
		}

	}
}
