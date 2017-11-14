using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FluentAssertions;
using WebForms.MVVM.Interfaces;
using System.Collections.Generic;
using WebForms.MVVM.Test.ObjetosTeste;
using WebForms.MVVM.Framework;

namespace WebForms.MVVM.Test.Framework
{
	[TestClass]
	public class ComparadorUnitTest
	{
		[TestMethod]
		public void AoVerificarSeEhControleDeEdicaoDeveRetornarVerdadeiro()
		{
			Comparador.EhControleDeEdicao(typeof(TextBox)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(Label)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(RadioButton)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(CheckBox)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(HiddenField)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(HtmlInputText)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(HtmlInputCheckBox)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(HtmlInputFile)).Should().BeTrue();
			Comparador.EhControleDeEdicao(typeof(HtmlInputHidden)).Should().BeTrue();
		}

		[TestMethod]
		public void AoVerificarSeEhControleDeEdicaoDeveRetornarFalso()
		{
			Comparador.EhControleDeEdicao(typeof(ComponenteTitulo)).Should().BeFalse();
			Comparador.EhControleDeEdicao(typeof(IControleTitulo)).Should().BeFalse();

			Comparador.EhControleDeEdicao(typeof(string)).Should().BeFalse();
			Comparador.EhControleDeEdicao(typeof(DateTime)).Should().BeFalse();
			Comparador.EhControleDeEdicao(typeof(System.Web.HttpCookie)).Should().BeFalse();
		}

		[TestMethod]
		public void AoVerificarSeEhUmaListaGenericaDeveRetornarVerdadeiro()
		{
			var lista = new List<string>();
			Comparador.EhUmaListaGenerica(lista.GetType()).Should().BeTrue();

			IList<string> iLista = lista;
			Comparador.EhUmaListaGenerica(iLista.GetType()).Should().BeTrue();
		}

		[TestMethod]
		public void AoVerificarSeEhUmaListaGenericaDeveRetornarFalso()
		{
			var lista = new string[] { };
			Comparador.EhUmaListaGenerica(lista.GetType()).Should().BeFalse();

			ICollection<string> iLista = lista;
			Comparador.EhUmaListaGenerica(iLista.GetType()).Should().BeFalse();
		}

		[TestMethod]
		public void AoVerificarSeEhComponenteTituloDeveRetornarValorEsperado()
		{
			Comparador.EhComponenteTitulo(typeof(ComponenteTitulo)).Should().BeTrue();
			Comparador.EhComponenteTitulo(typeof(ComponentePesquisa)).Should().BeFalse();

			Comparador.EhComponenteTitulo(typeof(IControleTitulo)).Should().BeTrue();
			Comparador.EhComponenteTitulo(typeof(IControlePesquisa)).Should().BeFalse();
		}

		[TestMethod]
		public void AoVerificarSeEhTipoBooleanDeveRetornarValorEsperado()
		{
			Comparador.EhTipoBoolean(typeof(bool)).Should().BeTrue();
			Comparador.EhTipoBoolean(typeof(string)).Should().BeFalse();
		}
	}
}
