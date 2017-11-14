using WebForms.MVVM.Attributes;

namespace WebForms.MVVM.Test.ObjetosTeste
{
	public class SubObjetoDeTestes
	{

		[Componente(CampoDados = "CodGrupoItens")]
		public int CodigoGrupoItens { get; set; }

		[Componente(CampoDados = "NomeGrupoItens")]
		public string NomeGrupoItens { get; set; }

		[Componente(CampoDados = "CodItem", GradeComComponente = true)]
		public int? CodigoItemPrincipal { get; set; }

	}
}
