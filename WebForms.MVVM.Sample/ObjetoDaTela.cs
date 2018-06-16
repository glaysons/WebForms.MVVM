using System.Collections.Generic;
using WebForms.MVVM.Attributes;

namespace WebForms.MVVM.Sample
{
	public class ObjetoDaTela
	{

		[Componente]
		public string Nome { get; set; }

		[Componente]
		public int? Idade { get; set; }

		[Componente]
		public string Endereco { get; set; }

		[Componente]
		public int CodigoCidade { get; set; }

		[Componente]
		public RegistroCivil EstadoCivil { get; set; }

		[Componente(PropriedadeDePesquisa = "CodigoCidade")]
		public string NomeCidade { get; set; }

		[Componente]
		public IList<ItemDaTela> Filhos { get; set; }

	}
}