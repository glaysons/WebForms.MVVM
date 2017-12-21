using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Framework;

namespace WebForms.MVVM.Atualizadores
{
	public class AtualizadorGrade
	{

		private DicionarioTela _dicionario;

		public AtualizadorGrade(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
		}

		public void AtualizarComponenteGrade(object objeto, PropertyInfo propriedade)
		{
			var grade = _dicionario.ConsultarComponenteEditor(propriedade, null);
			if ((grade == null) || !(grade is DataGrid))
			  return;
			var objetos = (objeto == null)
				? new List<object>()
				: ConsultarObjetosDaPropriedade(objeto, propriedade);
			PreencherGradeComSubObjetos(propriedade, (DataGrid)grade, objetos);
		}

		private IList ConsultarObjetosDaPropriedade(object objeto, PropertyInfo propriedade)
		{
			return (IList)propriedade.GetValue(objeto, null);
		}

		private void PreencherGradeComSubObjetos(PropertyInfo propriedade, DataGrid grade, IList objetos)
		{
			if (objetos == null)
				return;

			var tipoDoItem = Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedade);
			var propriedadesDoItemDaLista = tipoDoItem.GetProperties();
			var componentesDoItemDaLista = Consultador.ConsultarConfiguracaoDasPropriedades(propriedadesDoItemDaLista);

			using (var tabela = ConverterListaEmDataTable(componentesDoItemDaLista, objetos.Count))
			{
				grade.DataSource = tabela;
				grade.DataBind();
				PreencherItemDaGrade(propriedade, grade, objetos, propriedadesDoItemDaLista, componentesDoItemDaLista);
			}
		}

		private DataTable ConverterListaEmDataTable(IList<ComponenteAttribute> componentesDoItemDaLista, int numeroDeRegistros)
		{
			var tabela = new DataTable();

			for (var indice = 0; indice <= componentesDoItemDaLista.Count - 1; indice++)
			{
				var componente = componentesDoItemDaLista[indice];

				if (componente == null)
					continue;

				if (!string.IsNullOrEmpty(componente.CampoDados))
					tabela.Columns.Add(componente.CampoDados);
			}

			for (var registro = 1; registro <= numeroDeRegistros; registro++)
				tabela.Rows.Add(tabela.NewRow());

			return tabela;
		}

		private void PreencherItemDaGrade(PropertyInfo propriedadePrincipal, DataGrid grade, IList objetos, PropertyInfo[] propriedadesDoItemDaLista, IList<ComponenteAttribute> componentesDoItemDaLista)
		{
			var indiceGrade = 0;
			foreach (object objeto in objetos)
			{
				for (var indice = 0; indice <= componentesDoItemDaLista.Count - 1; indice++)
				{
					var componente = componentesDoItemDaLista[indice];

					if (componente == null)
						continue;

					var valorPropriedade = propriedadesDoItemDaLista[indice].GetValue(objeto, null);


					if (componente.GradeComComponente)
					{
						var objetoColuna = _dicionario.ConsultarObjetoEditorNoContainer(grade.Items[indiceGrade], propriedadePrincipal.Name + "." + propriedadesDoItemDaLista[indice].Name);
						if (objetoColuna != null)
							AtualizadorObjetos.Atualizar(objetoColuna, string.IsNullOrEmpty(componente.FormatoGrade) ? valorPropriedade : string.Format(componente.FormatoGrade, valorPropriedade));
					}

					else if (!string.IsNullOrEmpty(componente.CampoDados))
					{
						var indiceColuna = Consultador.ConsultarIndiceDaBoundColumnDaGrade(grade, componente.CampoDados);
						if (indiceColuna > -1)
							grade.Items[indiceGrade].Cells[indiceColuna].Text = string.IsNullOrEmpty(componente.FormatoGrade) ? Convert.ToString(valorPropriedade) : string.Format(componente.FormatoGrade, valorPropriedade);
					}
				}
				indiceGrade += 1;
			}
		}

	}
}
