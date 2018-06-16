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

			if (objeto is IControle)
				return ConsultarValorDoObjetoPersonalizado((IControle)objeto, textoRelacionado);

			if (objeto is WebControl)
				return ConsultarValorDoObjetoWebControl(objeto, textoRelacionado);

			if (objeto is HtmlControl)
				return ConsultarValorDoObjetoHtmlControl(objeto, textoRelacionado);

			throw new Exception("Não é possível realizar a leitura de valores de objetos do tipo [" + objeto.GetType().Name + "]");
		}

		private static object ConsultarValorDoObjetoPersonalizado(IControle objeto, bool textoRelacionado)
		{
			if ((textoRelacionado) && (objeto is IControlePesquisa))
				return ((IControlePesquisa)objeto).TextoResultado;
			return objeto.Valor;
		}

		private static object ConsultarValorDoObjetoWebControl(Control objeto, bool textoRelacionado)
		{
			if (objeto is TextBox)
				return ((TextBox)objeto).Text;

			if (objeto is CheckBox)
				return ((CheckBox)objeto).Checked;

			if (objeto is ListControl)
			{
				var lista = ((ListControl)objeto);
				if (textoRelacionado)
					return lista.SelectedItem?.Text ?? string.Empty;
				return lista.SelectedValue;
			}

			if (objeto is HiddenField)
				return ((HiddenField)objeto).Value;

			if (objeto is Label)
				return ((Label)objeto).Text;

			if (objeto is FileUpload)
			{
				var arquivo = ((FileUpload)objeto);
				if (textoRelacionado)
					return arquivo.FileName;
				return ((FileUpload)objeto).FileContent;
			}

			throw new Exception("O tipo WebControl [" + objeto.GetType().Name + "] não é suportado!");
		}

		private static object ConsultarValorDoObjetoHtmlControl(Control objeto, bool textoRelacionado)
		{
			if (objeto is HtmlInputHidden)
				return ((HtmlInputHidden)objeto).Value;

			if (objeto is HtmlInputText)
				return ((HtmlInputText)objeto).Value;

			if (objeto is HtmlInputCheckBox)
				return ((HtmlInputCheckBox)objeto).Value;

			if (objeto is HtmlSelect)
			{
				var dropdown = ((HtmlSelect)objeto);
				if (dropdown.SelectedIndex > -1)
				{
					if (textoRelacionado)
						return dropdown.Items[dropdown.SelectedIndex].Text;
					return dropdown.Items[dropdown.SelectedIndex].Value;
				}
				if (textoRelacionado)
					return string.Empty;
				return null;
			}

			if (objeto is HtmlInputFile)
			{
				var arquivo = ((HtmlInputFile)objeto).PostedFile;
				if (textoRelacionado)
					return arquivo.FileName;
				return arquivo.InputStream;
			}

			if (objeto is HtmlInputPassword)
				return ((HtmlInputPassword)objeto).Value;

			throw new Exception("O tipo WebControl [" + objeto.GetType().Name + "] não é suportado!");
		}

	}
}
