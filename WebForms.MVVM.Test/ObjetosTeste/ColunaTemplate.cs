using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms.MVVM.Test.ObjetosTeste
{
	public class ColunaTemplate : ITemplate
	{

		ListItemType _tipoTemplate;
		string _tituloColuna;

		public IList<Control> Controles { get; set; }

		public ColunaTemplate(ListItemType tipoTemplate, string tituloColuna)
		{
			_tipoTemplate = tipoTemplate;
			_tituloColuna = tituloColuna;
			Controles = new List<Control>();
		}

		public void InstantiateIn(Control container)
		{
			if (_tipoTemplate == ListItemType.Header)
			{
				var titulo = new Literal();
				titulo.Text = "<b>" + _tituloColuna + "</b>";
				container.Controls.Add(titulo);
			}

			foreach (var controle in Controles)
				container.Controls.Add(controle);
		}
	}
}
