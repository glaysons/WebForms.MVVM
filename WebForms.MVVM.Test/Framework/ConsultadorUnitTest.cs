using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.UI.WebControls;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForms.MVVM.Test.ObjetosTeste;
using WebForms.MVVM.Framework;

namespace WebForms.MVVM.Test.Framework
{
	[TestClass]
	public class ConsultadorTest
	{
		[TestMethod]
		public void SeConsultarConfiguracaoDaPropriedadeDeveRetornarComponenteAttributeValido()
		{
			var tipo = typeof(ObjetoDeTestes);
			var propriedade = tipo.GetProperty("CodigoFamiliaItens");
			var componente = Consultador.ConsultarConfiguracaoDaPropriedade(propriedade);

			componente
				.Should().NotBeNull();

			componente.Titulo
				.Should().Be("Família de Itens");

			componente.CampoDados
				.Should().Be("CodFamiliaItens");
		}

		[TestMethod]
		public void SeConsultarConfiguracaoDaExpressaoDeveEncontrarPropriedadeCorreta()
		{
			var tipo = typeof(ObjetoDeTestes);

			Expression<Func<ObjetoDeTestes, object>> consultadorSimples = o => o.CodigoFamiliaItens;
			var propriedadeSimples = tipo.GetProperty("CodigoFamiliaItens");
			var propriedadeSimplesDoConsultador = Consultador.ConsultarConfiguracaoDaExpressao(consultadorSimples);

			propriedadeSimplesDoConsultador
				.Should().NotBeNull();

			propriedadeSimplesDoConsultador
				.Should().BeSameAs(propriedadeSimples);

			Expression<Func<ObjetoDeTestes, IList<SubObjetoDeTestes>>> consultadorLista = o => o.GruposItens;
			var propriedadeLista = tipo.GetProperty("GruposItens");
			var propriedadeListaDoConsultador = Consultador.ConsultarConfiguracaoDaExpressao(consultadorLista);

			propriedadeListaDoConsultador
				.Should().NotBeNull();

			propriedadeListaDoConsultador
				.Should().BeSameAs(propriedadeLista);
		}

		[TestMethod]
		public void SeConsultarTipoEspecificoDaPropriedadeGenericaDeveRetornarUmTipoValido()
		{
			var tipo = typeof(ObjetoDeTestes);
			var propriedade = tipo.GetProperty("GruposItens");

			propriedade
				.Should().NotBeNull();

			propriedade.PropertyType
				.Should()
				.Be(typeof(IList<SubObjetoDeTestes>));

			var tipoEspecifico = Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedade);

			tipoEspecifico
				.Should().NotBeNull();

			tipoEspecifico
				.Should().Be(typeof(SubObjetoDeTestes));

			var propriedadeSimples = tipo.GetProperty("CodigoFamiliaItens");

			Consultador.ConsultarTipoEspecificoDaPropriedadeGenerica(propriedadeSimples)
				.Should().BeNull();
		}

		[TestMethod]
		public void SeConsultarIndiceDaBoundColumnDaGradeDeveInformarNumeroCorreto()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();

			var grade = (DataGrid)pagina.FindControl("grdGrupos");

			grade
				.Should().NotBeNull();

			Consultador.ConsultarIndiceDaBoundColumnDaGrade(grade, "CodFamiliaItens")
				.Should().Be(-1);

			Consultador.ConsultarIndiceDaBoundColumnDaGrade(grade, "CodGrupoItens")
				.Should().Be(0);

			Consultador.ConsultarIndiceDaBoundColumnDaGrade(grade, "NomeGrupoItens")
				.Should().Be(1);

			Consultador.ConsultarIndiceDaBoundColumnDaGrade(grade, "CodItem")
				.Should().Be(-1);

		}

	}
}
