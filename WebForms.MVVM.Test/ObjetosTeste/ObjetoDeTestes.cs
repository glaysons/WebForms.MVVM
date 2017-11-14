using System.Collections.Generic;
using WebForms.MVVM.Attributes;

namespace WebForms.MVVM.Test.ObjetosTeste
{
	public class ObjetoDeTestes
	{

		[Componente(Titulo = "Família de Itens", CampoDados = "CodFamiliaItens")]
		public int CodigoFamiliaItens { get; set; }

		[Componente(CampoDados = "opcaoNumerica")]
		public EnumDeTestes OpcaoNumerica { get; set; }

		[Componente(CampoDados = "opcaoTexto")]
		public EnumStringDeTestes OpcaoTexto { get; set; }

		[Componente(CampoDados = "Idade")]
		public int? Idade { get; set; }

		[Componente(CampoDados = "CodGrupoItens")]
		public int? CodigoGrupoItens { get; set; }

		[Componente()]
		public IList<SubObjetoDeTestes> GruposItens { get; set; }

		[Componente(CampoDados = "Nome")]
		public string Nome { get; set; }

		public int? PropriedadeNaoConfigurada { get; set; }

	}
}
