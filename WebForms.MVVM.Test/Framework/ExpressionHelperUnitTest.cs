using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForms.MVVM.Test.ObjetosTeste;
using WebForms.MVVM.Framework;
using FluentAssertions;
using System.Linq;

namespace WebForms.MVVM.Test.Framework
{
	[TestClass]
	public class ExpressionHelperUnitTest
	{

		[TestMethod]
		public void SeConsultarOCaminhoDaExpressaoDeveRetornarUmaStringComAsPropriedadesSeparadasPorPonto()
		{
			ExpressionHelper.CamihoDaExpressao<ObjetoDeTestes>(o => o.CodigoFamiliaItens)
				.Should()
				.Be("CodigoFamiliaItens");

			ExpressionHelper.CamihoDaExpressao<ObjetoDeTestes>(o => o.GruposItens)
				.Should()
				.Be("GruposItens");

			ExpressionHelper.CamihoDaExpressao<ObjetoDeTestes>(o => o.GruposItens.Select(f => f.NomeGrupoItens))
				.Should()
				.Be("GruposItens.NomeGrupoItens");
		}

	}
}
