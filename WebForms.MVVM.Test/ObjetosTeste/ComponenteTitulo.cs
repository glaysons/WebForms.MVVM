using System.Web.UI;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM.Test.ObjetosTeste
{
	public class ComponenteTitulo : UserControl, IControleTitulo
	{

		public string Texto { get; set; }

	}
}
