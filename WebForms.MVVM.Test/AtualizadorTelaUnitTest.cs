using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForms.MVVM.Test.ObjetosTeste;
using System.Web.UI.WebControls;
using FluentAssertions;
using WebForms.MVVM.Interfaces;
using System.Collections.Generic;
using System;

namespace WebForms.MVVM.Test
{
	[TestClass]
	public class AtualizadorTelaUnitTest
	{

		[TestMethod]
		public void SeCriarAtualizadorTelaSemDicionarioDeveGerarErro()
		{
			Action criarAtualizador = () => new AtualizadorTela(null);
			criarAtualizador
				.ShouldThrow<Exception>()
				.WithMessage("Favor informar um dicionario válido!");
		}

		[TestMethod]
		public void SeAtualizarTelaDevePreencherOsControles()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);
			var objeto = CriarObjetoPreenchidoParaTestes();

			new AtualizadorTela(dicionario).Atualizar(objeto);

			pagina
				.Should()
				.NotBeNull();

			var grade = (DataGrid)pagina.FindControl("grdGrupos");
			grade
				.Should().NotBeNull();

			grade.Items
				.Should().HaveCount(3, "Porque o DataGrid deve conter 3 itens!");

			var grupo = 1;
			foreach (DataGridItem item in grade.Items)
			{
				item.Cells[0].Text
					.Should().Be(grupo.ToString());

				item.Cells[1].Text
					.Should().Be(string.Concat("Grupo número ", grupo));

				item.Cells[3].FindControl("txtCodItem")
					.Should().NotBeNull()
					.And
					.BeAssignableTo<IControlePesquisa>();

				((IControlePesquisa)item.Cells[3].Controls[0]).Valor
					.Should().Be((grupo * 10).ToString());

				grupo++;
			}

			var radioNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");
			radioNumerico
				.Should().NotBeNull();

			radioNumerico.SelectedValue
				.Should().Be("2");

			var radioTexto = (RadioButtonList)pagina.FindControl("radOpcaoTexto");
			radioTexto
				.Should().NotBeNull();

			radioTexto.SelectedValue
				.Should().Be("C");
		}

		private ObjetoDeTestes CriarObjetoPreenchidoParaTestes()
		{
			return new ObjetoDeTestes()
			{
				CodigoFamiliaItens = 123,
				OpcaoNumerica = EnumDeTestes.Opcao2,
				OpcaoTexto = EnumStringDeTestes.OpcaoC,
				GruposItens = new List<SubObjetoDeTestes>()
				{
					new SubObjetoDeTestes()
					{
						CodigoGrupoItens = 1,
						NomeGrupoItens = "Grupo número 1",
						CodigoItemPrincipal = 10
					},

					new SubObjetoDeTestes()
					{
						CodigoGrupoItens = 2,
						NomeGrupoItens = "Grupo número 2",
						CodigoItemPrincipal = 20
					},

					new SubObjetoDeTestes()
					{
						CodigoGrupoItens = 3,
						NomeGrupoItens = "Grupo número 3",
						CodigoItemPrincipal = 30
					}
				}
			};
		}

		[TestMethod]
		public void SeLimparTelaDeveLimparOsControlesCorretamente()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);
			var objeto = CriarObjetoPreenchidoParaTestes();

			new AtualizadorTela(dicionario).Limpar<ObjetoDeTestes>();

			pagina
				.Should()
				.NotBeNull();

			var grade = (DataGrid)pagina.FindControl("grdGrupos");

			grade
				.Should().NotBeNull();

			grade.Items
				.Should().HaveCount(0, "Porque o DataGrid deve estar vazio!");

			var radioNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");

			radioNumerico
				.Should().NotBeNull();

			radioNumerico.SelectedIndex
				.Should().Be(-1);

			var radioTexto = (RadioButtonList)pagina.FindControl("radOpcaoTexto");
			radioTexto
				.Should().NotBeNull();

			radioTexto.SelectedIndex
				.Should().Be(-1);
		}

	}
}
