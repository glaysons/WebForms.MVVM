using System.Web.UI;

namespace WebForms.MVVM.Interfaces
{
	public interface ILayoutObjeto
	{

		bool Editavel { get; set; }

		bool Visivel { get; set; }

		string CssClass { get; set; }

		Control Controle { get; }

		string ConsultarAtributo(string nome);

		void DefinirAtributo(string nome, string valor);

	}
}
