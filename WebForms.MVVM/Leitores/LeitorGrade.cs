using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Framework;

namespace WebForms.MVVM.Leitores
{
	public class LeitorGrade
	{

		private DicionarioTela _dicionario;

		public LeitorGrade(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
		}

		public void PreencherPropriedadeLista(object objeto, PropertyInfo propriedade, ComponenteAttribute componente)
		{
			InstanciarPropriedadeDoObjeto(objeto, propriedade);
			var lista = ConsultarPropriedadeListaDoObjeto(objeto, propriedade);
			var grade = _dicionario.ConsultarComponenteEditor(propriedade, componente);
			if ((grade != null) && (grade is DataGrid))
				PreencherPropriedadeListaComBaseNumDataGrid((DataGrid)grade, lista, propriedade);
		}

		public static void InstanciarPropriedadeDoObjeto(object objeto, PropertyInfo propriedade)
		{
			var tipoDaLista = propriedade.PropertyType.GetGenericArguments().First();
			var tipoLista = typeof(List<>).MakeGenericType(tipoDaLista);
			object lista = Activator.CreateInstance(tipoLista);
			propriedade.SetValue(objeto, lista, null);
		}

		public static IList ConsultarPropriedadeListaDoObjeto(object objeto, PropertyInfo propriedade)
		{
			return (IList)propriedade.GetValue(objeto, null);
		}

		private void PreencherPropriedadeListaComBaseNumDataGrid(DataGrid grade, IList lista, PropertyInfo propriedade)
		{
			var tipoDaLista = Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedade);
			var propriedadesDoItemDaLista = tipoDaLista.GetProperties();
			var componentesDoItemDaLista = Consultador.ConsultarConfiguracaoDasPropriedades(propriedadesDoItemDaLista);
			foreach (DataGridItem itemDaGrade in grade.Items)
			{
				var itemDaLista = Activator.CreateInstance(tipoDaLista);
				PreencheItemDaListaComBaseNumDataGridItem(propriedade, grade, itemDaLista, propriedadesDoItemDaLista, componentesDoItemDaLista, itemDaGrade);
				lista.Add(itemDaLista);
			}
		}

		private void PreencheItemDaListaComBaseNumDataGridItem(PropertyInfo propriedadePrincipal, DataGrid grade, object itemDaLista, PropertyInfo[] propriedadesDoItemDaLista, IList<ComponenteAttribute> componentesDoItemDaLista, DataGridItem itemDaGrade)
		{
			for (var indice = 0; indice <= propriedadesDoItemDaLista.Length - 1; indice++)
			{
				if (componentesDoItemDaLista[indice] == null)
					continue;

				var propriedade = propriedadesDoItemDaLista[indice];
				var componente = componentesDoItemDaLista[indice];
				object valorDaColuna = null;

				if (componente.GradeComComponente)
				{
					var componenteColuna = _dicionario.ConsultarObjetoEditorNoContainer(itemDaGrade, propriedadePrincipal.Name + "." + propriedade.Name);
					if (componenteColuna != null)
					{
						try
						{
							if (Comparador.EhCampoNullable(propriedade.PropertyType))
								valorDaColuna = Convert.ChangeType(LeitorObjetos.Ler(componenteColuna), Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedade));
							else
								valorDaColuna = Convert.ChangeType(LeitorObjetos.Ler(componenteColuna), propriedade.PropertyType);
						}
						catch
						{
						}
					}
				}

				else if (!string.IsNullOrEmpty(componente.CampoDados))
				{
					var indiceColuna = Consultador.ConsultarIndiceDaBoundColumnDaGrade(grade, componente.CampoDados);
					if (indiceColuna > -1)
					{
						try
						{
							valorDaColuna = Convert.ChangeType(itemDaGrade.Cells[indiceColuna].Text, propriedade.PropertyType);
						}
						catch
						{
						}
					}
				}

				propriedade.SetValue(itemDaLista, valorDaColuna, null);
			}
		}

	}
}
