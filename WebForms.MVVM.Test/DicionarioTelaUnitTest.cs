using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Web.UI;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Test.ObjetosTeste;

namespace WebForms.MVVM.Test
{
	[TestClass]
	public class DicionarioTelaUnitTest
	{

		[TestMethod]
		public void SeCriarUmDicionarioDaPaginaDeveEncontrarComponentesCriados()
		{
			var tela = CriarDicionario();

			tela.Dicionario
				.Should()
				.NotBeNull();

			tela.Dicionario.Count
				.Should().BeGreaterOrEqualTo(5);

			tela.Dicionario["codigofamiliaitens"]
				.Should().HaveCount(2);

			tela.Dicionario["opcaonumerica"]
				.Should().HaveCount(2);

			tela.Dicionario["opcaotexto"]
				.Should().HaveCount(2);

			tela.Dicionario["grupositens.codigogrupoitens"]
				.Should().HaveCount(2);

			tela.Dicionario["grupositens"]
				.Should().HaveCount(1);
		}

		private static DicionarioTela CriarDicionario()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			return new DicionarioTela(pagina, WebFormFactory.NOMETAG);
		}

		[TestMethod]
		public void SeConsultarComponenteTituloDeveLocalizarComponente()
		{
			var tela = CriarDicionario();

			var titulo = tela.ConsultarComponenteTitulo("CodigoFamiliaItens");

			titulo
				.Should()
				.NotBeNull();

			titulo
				.Should()
				.BeOfType<ComponenteTitulo>();

			var controle = (ComponenteTitulo)titulo;
			controle.ID
				.Should()
				.Be("lblFamiliaItens");

			controle
				.Should()
				.Be(titulo);
		}

		[TestMethod]
		public void SeConsultarComponenteTituloDeUmaPropriedadeSemTituloDeveRetornarNulo()
		{
			var tela = CriarDicionario();

			var titulo = tela.ConsultarComponenteTitulo("PropriedadeNaoConfigurada");

			titulo
				.Should()
				.BeNull();
		}

		[TestMethod]
		public void SeConsultarComponenteEdicaoDeUmaPropriedadeSemComponenteDeveRetornarNulo()
		{
			var tela = CriarDicionario();

			var edicao = tela.ConsultarComponenteEditor("PropriedadeNaoConfigurada");

			edicao
				.Should()
				.BeNull();
		}

		[TestMethod]
		public void SeConsultarComponenteEditorPorPropriedadeDeveLocalizarComponente()
		{
			var tela = CriarDicionario();
			var tipo = typeof(ObjetoDeTestes);
			var propriedade = tipo.GetProperty("CodigoFamiliaItens");

			var editor = tela.ConsultarComponenteEditor(propriedade, null);

			editor
				.Should()
				.NotBeNull();

			editor
				.Should()
				.BeOfType<ComponentePesquisa>();

			var controle = (ComponentePesquisa)editor;
			controle.ID
				.Should()
				.Be("txtFamiliaItens");

			controle
				.Should()
				.Be(editor);
		}

		[TestMethod]
		public void SeConsultarComponenteEditorPorPropriedadeSubstituindoAPrincipalDeveLocalizarComponente()
		{
			var tela = CriarDicionario();
			var tipo = typeof(ObjetoDeTestes);
			var propriedade = tipo.GetProperty("CodigoFamiliaItens");
			var componente = new ComponenteAttribute()
			{
				PropriedadeDePesquisa = "CodigoFamiliaItens"
			};

			var editor = tela.ConsultarComponenteEditor(propriedade, componente);

			editor
				.Should()
				.NotBeNull();

			editor
				.Should()
				.BeOfType<ComponentePesquisa>();

			var controle = (ComponentePesquisa)editor;
			controle.ID
				.Should()
				.Be("txtFamiliaItens");

			controle
				.Should()
				.Be(editor);

			controle
				.Should()
				.Be(editor);
		}

		[TestMethod]
		public void SeConsultarComponenteEditorDeveLocalizarComponente()
		{
			var tela = CriarDicionario();

			var editor = tela.ConsultarComponenteEditor("CodigoFamiliaItens");

			editor
				.Should()
				.NotBeNull();

			editor
				.Should()
				.BeOfType<ComponentePesquisa>();

			var controle = (ComponentePesquisa)editor;
			controle.ID
				.Should()
				.Be("txtFamiliaItens");

			controle
				.Should()
				.Be(editor);
		}

		[TestMethod]
		public void SeConsultarObjetoTituloNoContainerDeveEncontrarComponente()
		{
			var componente = new ComponenteTitulo().Tag("CodigoFamiliaItens");
			var container = new Control();
			container.Controls.Add(new Control());
			container.Controls[0].Controls.Add(new Control());
			container.Controls[0].Controls[0].Controls.Add(componente);

			var dicionario = new DicionarioTela(null, WebFormFactory.NOMETAG);
			var componenteEncontrado = dicionario.ConsultarObjetoTituloNoContainer(container, "CodigoFamiliaItens");

			componenteEncontrado
				.Should()
				.NotBeNull();

			componenteEncontrado
				.Should()
				.Be(componente);
		}

		[TestMethod]
		public void SeConsultarObjetoEditorNoContainerDeveEncontrarComponente()
		{
			var componente = new ComponentePesquisa().Tag("CodigoFamiliaItens");
			var container = new Control();

			container.Controls.Add(new Control());
			container.Controls[0].Controls.Add(new Control());
			container.Controls[0].Controls[0].Controls.Add(componente);

			var dicionario = new DicionarioTela(null, WebFormFactory.NOMETAG);
			var componenteEncontrado = dicionario.ConsultarObjetoEditorNoContainer(container, "CodigoFamiliaItens");

			componenteEncontrado
				.Should()
				.NotBeNull();

			componenteEncontrado
				.Should()
				.Be(componente);
		}

		[TestMethod]
		public void SeAtivarCaminhoRaizDeveSerPossivelLocalizarComUmaRaizAtiva()
		{
			var tipo = typeof(SubObjetoDeTestes);
			var propriedade = tipo.GetProperty("CodigoGrupoItens");

			var componente = new ComponentePesquisa().Tag("PropriedadeRaiz.CodigoGrupoItens");
			var container = new Control();

			container.Controls.Add(new Control());
			container.Controls[0].Controls.Add(new Control());
			container.Controls[0].Controls[0].Controls.Add(componente);

			var dicionario = new DicionarioTela(container, WebFormFactory.NOMETAG);

			dicionario.AtivarCaminhoRaiz("PropriedadeRaiz");

			var componenteEncontrado = dicionario.ConsultarComponenteEditor(propriedade.Name);

			dicionario.DesativarCaminhoRaiz();

			componenteEncontrado
				.Should()
				.NotBeNull();

			componenteEncontrado
				.Should()
				.Be(componente);
		}

	}
}
