using System;
using WebForms.MVVM.Attributes;

namespace WebForms.MVVM.Sample
{
	public class ItemDaTela
	{

		[Componente]
		public string Nome { get; set; }

		[Componente]
		public int Idade { get; set; }

		[Componente(FormatoGrade = "{0:dd} de {0:MMMM} de {0:yyyy}")]
		public DateTime? DataDeNascimento { get; set; }

	}
}