using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM.Layouts
{
	public class LayoutObjeto : ILayoutObjeto
	{

		private Control _componente;

		public string ConsultarAtributo(string nome)
		{
			if (_componente is WebControl)
				return ((WebControl)_componente).Attributes[nome];

			if (_componente is HtmlControl)
				return ((HtmlControl)_componente).Attributes[nome];

			if ((_componente is UserControl))
			{
				return ((UserControl)_componente).Attributes[nome];
			}

			throw new Exception("O tipo do controle [" + _componente.GetType().Name + "] não é suportado para a propriedade [Atributo]!");
		}

		public void DefinirAtributo(string nome, string valor)
		{
			if (_componente is WebControl)
				((WebControl)_componente).Attributes[nome] = valor;

			else if (_componente is HtmlControl)
				((HtmlControl)_componente).Attributes[nome] = valor;

			else if (_componente is UserControl)
				((UserControl)_componente).Attributes[nome] = valor;

			else
				throw new Exception("O tipo do controle [" + _componente.GetType().Name + "] não é suportado para a propriedade [Atributo]!");
		}

		public string CssClass
		{
			get
			{
				if (_componente is WebControl)
					return ((WebControl)_componente).CssClass;

				if (_componente is HtmlControl)
					return ((HtmlControl)_componente).Attributes["class"];

				if ((_componente is UserControl))
					return ((UserControl)_componente).Attributes["class"];

				throw new Exception("O tipo do controle [" + _componente.GetType().Name + "] não é suportado para a propriedade [CssClass]!");
			}
			set
			{
				if (_componente is WebControl)
					((WebControl)_componente).CssClass = value;

				else if (_componente is HtmlControl)
					((HtmlControl)_componente).Attributes["class"] = value;

				else if (_componente is UserControl)
					((UserControl)_componente).Attributes["class"] = value;

				else
					throw new Exception("O tipo do controle [" + _componente.GetType().Name + "] não é suportado para a propriedade [CssClass]!");
			}
		}

		public bool Editavel
		{
			get
			{
				if (_componente is IControle)
					return ((IControle)_componente).Editavel;

				if (_componente is WebControl)
					return ((WebControl)_componente).Enabled;

				if (_componente is HtmlControl)
					return (!((HtmlControl)_componente).Disabled);

				throw new Exception("O tipo do controle [" + _componente.GetType().Name + "] não é suportado para a propriedade [Editavel]!");
			}
			set
			{
				if (_componente is IControle)
					((IControle)_componente).Editavel = value;

				else if (_componente is WebControl)
					((WebControl)_componente).Enabled = value;

				else if (_componente is HtmlControl)
					((HtmlControl)_componente).Disabled = (!value);

				else
					throw new Exception("O tipo do controle [" + _componente.GetType().Name + "] não é suportado para a propriedade [Editavel]!");
			}
		}

		public bool Visivel
		{
			get { return _componente.Visible; }
			set { _componente.Visible = value; }
		}

		public object Controle
		{
			get { return _componente; }
		}

		public LayoutObjeto(Control componente)
		{
			_componente = componente;
		}

	}
}
