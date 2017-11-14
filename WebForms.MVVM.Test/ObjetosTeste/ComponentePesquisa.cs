using System.Web.UI;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM.Test.ObjetosTeste
{
	public class ComponentePesquisa : UserControl, IControlePesquisa
	{

		public string Valor { get; set; }

		public string TextoResultado { get; set; }

	}
}
