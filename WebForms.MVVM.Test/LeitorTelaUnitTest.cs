using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForms.MVVM.Test.ObjetosTeste;
using System.Web.UI.WebControls;
using FluentAssertions;

namespace WebForms.MVVM.Test
{
	[TestClass]
	public class LeitorTelaUnitTest
	{
		[TestMethod]
		public void SeCriarPaginaDeTestesPreenchidaDoObjetoDeTestesDeveGerarPaginaValida()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesPreenchidaDoObjetoDeTestes(
				familiaItens: 123,
				quantidadeGrupos: 3,
				quantidadeDeAnos: 5);

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
					.BeOfType<ComponentePesquisa>();

				((ComponentePesquisa)item.Cells[3].Controls[0]).Valor
					.Should().Be((grupo * 10).ToString());

				grupo++;
			}

			var radioNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");
			radioNumerico
				.Should()
				.NotBeNull();

			radioNumerico.Items
				.Should()
				.HaveCount(3);
		}

		[TestMethod]
		public void SeCriarLeitorDePaginaSemDicionarioDeveGerarErro()
		{
			Action criarLeitor = () => new LeitorTela(null);
			criarLeitor
				.ShouldThrow<Exception>()
				.WithMessage("Favor informar um dicionario válido!");
		}

		[TestMethod]
		public void SeCriarPaginaPreenchidaDoObjetoDeveGerarValueObjectPreenchido()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesPreenchidaDoObjetoDeTestes(
				familiaItens: 123,
				quantidadeGrupos: 3,
				quantidadeDeAnos: 8);

			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);
			var objeto = new LeitorTela(dicionario).Ler<ObjetoDeTestes>();

			objeto.CodigoFamiliaItens
				.Should()
				.Be(123);

			objeto.GruposItens
				.Should().HaveCount(3);

			for (int indice = 1; indice <= objeto.GruposItens.Count; indice++)
			{
				var grupo = objeto.GruposItens[indice - 1];
				grupo.CodigoGrupoItens
					.Should().Be(indice);

				grupo.NomeGrupoItens
					.Should().Be(string.Concat("Grupo número ", indice));

				if (indice % 2 == 0)
					grupo.UmNumeroQualquer
						.Should().Be(indice * 10);
				else
					grupo.UmNumeroQualquer
						.Should().BeNull();

				grupo.CodigoItemPrincipal
					.Should().Be(indice * 10);
			}

			objeto.OpcaoNumerica
				.Should()
				.Be(EnumDeTestes.Opcao2);

			objeto.OpcaoTexto
				.Should()
				.Be(EnumStringDeTestes.OpcaoC);
		}
	}
}
