using System;
using System.Linq.Expressions;
using WebForms.MVVM.Framework;
using WebForms.MVVM.Interfaces;
using WebForms.MVVM.Layouts;

namespace WebForms.MVVM
{
	public class LayoutTela<TObjetoValor> : ILayoutTela<TObjetoValor> 
	{

		private DicionarioTela _dicionario;

		public LayoutTela(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
		}

		public ILayoutObjeto Componente(Expression<Func<TObjetoValor, object>> campo)
		{
			var propriedade = Consultador.ConsultarConfiguracaoDaExpressao(campo);
			var componente = Consultador.ConsultarConfiguracaoDaPropriedade(propriedade);
			var objeto = _dicionario.ConsultarComponenteEditor(propriedade, componente);
			if (objeto == null)
				throw new Exception("Não foi possível localizar um objeto editor para a propriedade [" + propriedade.Name + "]!");
			return new LayoutObjeto(objeto);
		}

	}
}
