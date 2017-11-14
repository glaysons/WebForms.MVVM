using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForms.MVVM.Test.ObjetosTeste;
using System.Web.UI.WebControls;
using WebForms.MVVM.Atualizadores;
using WebForms.MVVM.Framework;
using FluentAssertions;

namespace WebForms.MVVM.Test.Atualizadores
{
	[TestClass]
	public class AtualizadorEnumUnitTest
	{

		[TestMethod]
		public void SeAtualizarComponenteComBaseNumEnumDeOpcoesNumericasDeveAtribuirPropriedadeCorretamente()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);

			var objetoOrigem = new ObjetoDeTestes();
			objetoOrigem.OpcaoNumerica = EnumDeTestes.Opcao3;

			var propriedadeNumerica = typeof(ObjetoDeTestes).GetProperty("OpcaoNumerica");
			var configNumerico = Consultador.ConsultarConfiguracaoDaPropriedade(propriedadeNumerica);

			var atualizador = new AtualizadorEnum(dicionario);
			atualizador.PreencherPropriedadeEnum(objetoOrigem, propriedadeNumerica, configNumerico);

			var componenteNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");
			componenteNumerico.SelectedValue
				.Should()
				.Be("3");
		}

		[TestMethod]
		public void SeAtualizarComponenteComBaseNumEnumDeOpcoesTextoDeveAtribuirPropriedadeCorretamente()
		{
			var pagina = WebFormFactory.CriarPaginaDeTestesDoObjetoDeTestes();
			var dicionario = new DicionarioTela(pagina, WebFormFactory.NOMETAG);

			var objetoOrigem = new ObjetoDeTestes();
			objetoOrigem.OpcaoTexto = EnumStringDeTestes.OpcaoC;

			var propriedadeTexto = typeof(ObjetoDeTestes).GetProperty("OpcaoTexto");
			var configTexto = Consultador.ConsultarConfiguracaoDaPropriedade(propriedadeTexto);

			var atualizador = new AtualizadorEnum(dicionario);
			atualizador.PreencherPropriedadeEnum(objetoOrigem, propriedadeTexto, configTexto);

			var componenteTexto = (RadioButtonList)pagina.FindControl("radOpcaoTexto");
			componenteTexto.SelectedValue
				.Should()
				.Be("C");
		}

	}
}
