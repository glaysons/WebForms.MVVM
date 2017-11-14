using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using WebForms.MVVM.Leitores;
using FluentAssertions;
using WebForms.MVVM.Test.ObjetosTeste;

namespace WebForms.MVVM.Test.Leitores
{
	[TestClass]
	public class LeitorRegistrosUnitTest
	{
		[TestMethod]
		public void SeCriarUmLeitorDeDataRowDeveGerarUmObjetoCorretamente()
		{
			var registro = CriarRegistroParaLeitura();

			var objeto = new LeitorRegistros().Ler<ObjetoDeTestes>(registro);

			objeto.CodigoFamiliaItens
				.Should().Be(123);

			objeto.OpcaoNumerica
				.Should().Be(EnumDeTestes.Opcao2);

			objeto.OpcaoTexto
				.Should().Be(EnumStringDeTestes.OpcaoC);

			objeto.CodigoGrupoItens
				.Should().Be(456);
		}

		private DataRow CriarRegistroParaLeitura()
		{
			var tabela = CriarEstruturaDaTabelaParaLeitura();
			var registro = tabela.NewRow();
			registro["CodFamiliaItens"] = 123;
			registro["opcaoNumerica"] = 2;
			registro["opcaoTexto"] = "C";
			registro["CodGrupoItens"] = 456;
			tabela.Rows.Add(registro);
			return registro;
		}

		private DataTable CriarEstruturaDaTabelaParaLeitura()
		{
			var tabela = new DataTable();
			tabela.Columns.Add("CodFamiliaItens", typeof(int));
			tabela.Columns.Add("opcaoNumerica", typeof(int));
			tabela.Columns.Add("opcaoTexto", typeof(string));
			tabela.Columns.Add("CodGrupoItens", typeof(int));
			return tabela;
		}
	}
}
