using System;
using System.Linq.Expressions;

namespace WebForms.MVVM.Interfaces
{
	public interface ILayoutTela<TObjetoValor>
	{

		ILayoutObjeto Componente(Expression<Func<TObjetoValor, object>> campo);

	}
}
