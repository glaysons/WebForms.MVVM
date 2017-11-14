using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM.Leitores
{
	public class LeitorObjetos
	{

		public static object Ler(PropertyInfo propriedade, DicionarioTela dicionario, ComponenteAttribute componente)
		{
			var objeto = dicionario.ConsultarComponenteEditor(propriedade, componente);
			if (objeto == null)
				throw new Exception("Não foi possível encontrar algum um objeto editor vinculado a propriedade [" + propriedade.Name + "]!");
			return Ler(objeto, textoRelacionado: (!string.IsNullOrEmpty(componente.PropriedadeDePesquisa)));
		}

		public static object Ler(Control objeto, bool textoRelacionado = false)
		{
			if (objeto == null)
				return null;

			if (objeto is IControlePesquisa)
				return ConsultarValorDoObjetoPersonalizado((IControlePesquisa)objeto, textoRelacionado);

			if (objeto is WebControl)
				return ConsultarValorDoObjetoWebControl(objeto);

			if (objeto is HtmlControl)
				return ConsultarValorDoObjetoHtmlControl(objeto);

			throw new Exception("Não é possível realizar a leitura de valores de objetos do tipo [" + objeto.GetType().Name + "]");
		}

		private static object ConsultarValorDoObjetoPersonalizado(IControlePesquisa objeto, bool textoRelacionado)
		{
			if (textoRelacionado)
				return objeto.TextoResultado;
			return objeto.Valor;
		}

		private static object ConsultarValorDoObjetoWebControl(Control objeto)
		{
			if (objeto is TextBox)
				return ((TextBox)objeto).Text;

			if (objeto is CheckBox)
				return ((CheckBox)objeto).Checked;

			if (objeto is RadioButtonList)
				return ((RadioButtonList)objeto).SelectedValue;

			if (objeto is DropDownList)
				return ((DropDownList)objeto).SelectedValue;

			if (objeto is ListBox)
				return ((ListBox)objeto).SelectedValue;

			if (objeto is HiddenField)
				return ((HiddenField)objeto).Value;

			if (objeto is Label)
				return ((Label)objeto).Text;

			if (objeto is FileUpload)
				return ((FileUpload)objeto).FileContent;

			throw new Exception("O tipo WebControl [" + objeto.GetType().Name + "] não é suportado!");
		}

		private static object ConsultarValorDoObjetoHtmlControl(Control objeto)
		{
			if (objeto is HtmlInputHidden)
				return ((HtmlInputHidden)objeto).Value;

			if (objeto is HtmlInputText)
				return ((HtmlInputText)objeto).Value;

			if (objeto is HtmlInputCheckBox)
				return ((HtmlInputCheckBox)objeto).Value;

			if (objeto is HtmlInputFile)
				return ((HtmlInputFile)objeto).PostedFile.InputStream;

			if (objeto is HtmlInputPassword)
				return ((HtmlInputPassword)objeto).Value;

			throw new Exception("O tipo WebControl [" + objeto.GetType().Name + "] não é suportado!");
		}

	}
}
