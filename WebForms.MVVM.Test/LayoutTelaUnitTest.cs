using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForms.MVVM.Test.ObjetosTeste;
using System.Web.UI.WebControls;
using FluentAssertions;

namespace WebForms.MVVM.Test
{
	[TestClass]
	public class LayoutTelaUnitTest
	{
		[TestMethod]
		public void SeAlterarLayoutDeUmComponenteExistenteDeveMudarPropriedadeCorreta()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);

			var layout = new LayoutTela<ObjetoDeTestes>(dicionario);

			var componenteNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");

			layout.Componente(c => c.OpcaoNumerica).Visivel = true;

			componenteNumerico.Visible
				.Should()
				.BeTrue();

			layout.Componente(c => c.OpcaoNumerica).Visivel = false;

			componenteNumerico.Visible
				.Should()
				.BeFalse();

		}

		[TestMethod]
		public void SeAlterarLayoutDeUmComponenteInexistenteDeveGerarErro()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);
			var layout = new LayoutTela<ObjetoDeTestes>(dicionario);

			Action mudarVisibilidade = () => layout.Componente(c => c.PropriedadeNaoConfigurada).Visivel = true;

			mudarVisibilidade
				.ShouldThrow<Exception>()
				.WithMessage("Não foi possível localizar um objeto editor para a propriedade [PropriedadeNaoConfigurada]!");
		}
	}
}
