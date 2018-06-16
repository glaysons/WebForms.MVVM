using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM.Atualizadores
{
	public class AtualizadorObjetos
	{

		public static void Atualizar(string propriedade, DicionarioTela dicionario, ComponenteAttribute componente, object valor)
		{
			var propriedadeVinculada = componente.PropriedadeDePesquisa ?? propriedade;
			var objeto = dicionario.ConsultarComponenteEditor(propriedadeVinculada);
			if ((objeto == null) && (string.IsNullOrEmpty(componente.CampoDados)))
				throw new Exception("Não foi possível encontrar algum um objeto editor vinculado a propriedade [" + propriedadeVinculada + "]!");
			Atualizar(objeto, valor, textoRelacionado: (!string.IsNullOrEmpty(componente.PropriedadeDePesquisa)));
		}

		public static void Atualizar(Control objeto, object valor, bool textoRelacionado = false)
		{
			if (objeto == null)
				return;

			if (objeto is IControle)
				AtualizarValorDoObjetoPersonalizado((IControle)objeto, valor, textoRelacionado);

			else if (objeto is WebControl)
				AtualizarValorDoObjetoWebControl(objeto, valor);

			else if (objeto is HtmlControl)
				AtualizarValorDoObjetoHtmlControl(objeto, valor);

			else
				throw new Exception("Não é possível realizar a atualização de valores de objetos do tipo [" + objeto.GetType().Name + "]");
		}

		private static void AtualizarValorDoObjetoPersonalizado(IControle objeto, object valor, bool textoRelacionado)
		{
			if ((textoRelacionado) && (objeto is IControlePesquisa))
				((IControlePesquisa)objeto).TextoResultado = Convert.ToString(valor);
			else
				objeto.Valor = valor;
		}

		private static void AtualizarValorDoObjetoWebControl(Control objeto, object valor)
		{
			if (objeto is TextBox)
				((TextBox)objeto).Text = Convert.ToString(valor);

			else if (objeto is Label)
				((Label)objeto).Text = Convert.ToString(valor);

			else if (objeto is HiddenField)
				((HiddenField)objeto).Value = Convert.ToString(valor);

			else if (objeto is CheckBox)
				((CheckBox)objeto).Checked = Convert.ToBoolean(valor);

			else if (objeto is ListControl)
				if (valor == null)
					((ListControl)objeto).SelectedIndex = -1;
				else
					((ListControl)objeto).SelectedValue = Convert.ToString(valor);

			else if (objeto is Label)
				((Label)objeto).Text = Convert.ToString(valor);

			else
				throw new Exception("O tipo WebControl [" + objeto.GetType().Name + "] não é suportado!");
		}

		private static void AtualizarValorDoObjetoHtmlControl(Control objeto, object valor)
		{
			if (objeto is HtmlInputHidden)
				((HtmlInputHidden)objeto).Value = Convert.ToString(valor);

			else if (objeto is HtmlInputText)
				((HtmlInputText)objeto).Value = Convert.ToString(valor);

			else if (objeto is HtmlInputCheckBox)
				((HtmlInputCheckBox)objeto).Value = Convert.ToBoolean(valor) ? "checked" : string.Empty;

			else if ((objeto is HtmlInputRadioButton))
				((HtmlInputRadioButton)objeto).Value = Convert.ToBoolean(valor) ? "checked" : string.Empty;

			else
				throw new Exception("O tipo HtmlControl [" + objeto.GetType().Name + "] não é suportado!");
		}

	}
}
