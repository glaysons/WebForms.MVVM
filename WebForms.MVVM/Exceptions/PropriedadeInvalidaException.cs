using System;

namespace WebForms.MVVM.Exceptions
{
	public class PropriedadeInvalidaException : Exception
	{
		public PropriedadeInvalidaException(string nome)
			: base(string.Concat("A propriedade [", nome, "] não é uma coleção! Favor informar uma propriedade válida!"))
		{

		}
	}
}
