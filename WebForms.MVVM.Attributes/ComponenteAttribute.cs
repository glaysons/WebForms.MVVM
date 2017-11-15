using System;

namespace WebForms.MVVM.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class ComponenteAttribute : Attribute
	{

		public virtual string PropriedadeDePesquisa { get; set; }

		public virtual bool GradeComComponente { get; set; }

		public virtual string CampoDados { get; set; }

		public virtual string FormatoGrade { get; set; }

	}
}
