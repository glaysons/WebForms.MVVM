using System;
using System.Reflection;
using System.Web.UI;

namespace WebForms.MVVM.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class ComponenteAttribute : Attribute
	{

		public virtual string Titulo { get; set; }

		public virtual bool Requerido { get; set; }

		public virtual bool Editavel { get; set; }

		public virtual int TamanhoMaximo { get; set; }

		public virtual int Colunas { get; set; }

		public object ValorPadrao { get; set; }

		public virtual string FuncaoPadronizadora { get; set; }

		public virtual string PropriedadeDePesquisa { get; set; }

		public virtual bool GradeComComponente { get; set; }

		public virtual string CampoDados { get; set; }

		public virtual string FormatoGrade { get; set; }

		public ComponenteAttribute()
		{
			Editavel = true;
		}

		public virtual void PersonalizarParametrosDoMetodo(Control componenteTitulo, Control componenteEdicao, ParameterInfo[] parametros, object[] valores)
		{
		}

	}
}
