using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Interfaces;

namespace WebForms.MVVM
{
	public class DicionarioTela
	{

		private IDictionary<string, IList<Control>> _dicionario;
		private string _nomeTag;
		private string _caminhoRaiz;

		public IDictionary<string, IList<Control>> Dicionario
		{
			get { return _dicionario; }
		}

		public DicionarioTela(Control container, string nomeTag)
		{
			_dicionario = new Dictionary<string, IList<Control>>();
			_nomeTag = nomeTag;
			CatalogarComponentes(container);
		}

		private void CatalogarComponentes(Control container)
		{
			if (container == null)
				return;
			var propriedade = ConsultarNomePropriedadeVinculada(container);
			if (!string.IsNullOrEmpty(propriedade))
				CatalogarComponente(propriedade, container);
			foreach (Control controle in container.Controls)
				CatalogarComponentes(controle);
		}

		private string ConsultarNomePropriedadeVinculada(Control componente)
		{
			if (componente is UserControl)
				return ((UserControl)componente).Attributes[_nomeTag];
			if (componente is WebControl)
				return ((WebControl)componente).Attributes[_nomeTag];
			if (componente is HtmlControl)
				return ((HtmlControl)componente).Attributes[_nomeTag];
			return string.Empty;
		}

		private void CatalogarComponente(string propriedade, Control componente)
		{
			var chave = ConsultarChave(propriedade);
			if (!_dicionario.ContainsKey(chave))
				_dicionario.Add(chave, new List<Control>());
			_dicionario[chave].Add(componente);
		}

		private string ConsultarChave(string propriedade, ComponenteAttribute componente = null)
		{
			return (string.Concat(_caminhoRaiz, componente?.PropriedadeDePesquisa ?? propriedade)).Trim().ToLower();
		}

		public IControleTitulo ConsultarComponenteTitulo(string propriedade)
		{
			foreach (IControleTitulo componente in ConsultarComponentesTitulos(propriedade))
				return componente;
			return null;
		}

		private IEnumerable<IControleTitulo> ConsultarComponentesTitulos(string propriedade)
		{
			var chave = ConsultarChave(propriedade);
			if (_dicionario.ContainsKey(chave))
				foreach (Control objeto in _dicionario[chave])
					if (objeto is IControleTitulo)
						yield return (IControleTitulo)objeto;
		}

		public Control ConsultarComponenteEditor(PropertyInfo propriedade, ComponenteAttribute componente)
		{
			var chave = ConsultarChave(propriedade, componente);
			foreach (Control objeto in ConsultarComponentesEditores(chave))
				return objeto;
			return null;
		}

		private IEnumerable<Control> ConsultarComponentesEditores(string chave)
		{
			if (_dicionario.ContainsKey(chave))
				foreach (Control objeto in _dicionario[chave])
					if (!(objeto is IControleTitulo))
						yield return objeto;
		}

		private string ConsultarChave(PropertyInfo propriedade, ComponenteAttribute componente = null)
		{
			return (string.Concat(_caminhoRaiz, componente?.PropriedadeDePesquisa ?? propriedade.Name)).Trim().ToLower();
		}

		public Control ConsultarComponenteEditor(string propriedade)
		{
			var chave = ConsultarChave(propriedade);
			foreach (Control objeto in ConsultarComponentesEditores(chave))
				return objeto;
			return null;
		}

		public Control ConsultarObjetoTituloNoContainer(Control container, string propriedade)
		{
			if (container == null)
				return null;

			var chave = ConsultarNomePropriedadeVinculada(container);

			if (string.Equals(chave, propriedade, StringComparison.InvariantCultureIgnoreCase))
			{
				if (container is IControleTitulo)
					return container;
				return null;
			}

			foreach (Control controle in container.Controls)
			{
				var controleEncontrado = ConsultarObjetoTituloNoContainer(controle, propriedade);
				if (controleEncontrado != null)
					return controleEncontrado;
			}

			return null;
		}

		public Control ConsultarObjetoEditorNoContainer(Control container, string propriedade)
		{
			if (container == null)
				return null;

			var chave = ConsultarNomePropriedadeVinculada(container);

			if (string.Equals(chave, propriedade, StringComparison.InvariantCultureIgnoreCase))
			{
				if (container is IControleTitulo)
					return null;
				return container;
			}

			foreach (Control controle in container.Controls)
			{
				var controleEncontrado = ConsultarObjetoEditorNoContainer(controle, propriedade);
				if (controleEncontrado != null)
					return controleEncontrado;
			}

			return null;
		}

		public void AtivarCaminhoRaiz(string caminho)
		{
			if (!string.IsNullOrEmpty(caminho))
				_caminhoRaiz = caminho + ".";
		}

		public void DesativarCaminhoRaiz()
		{
			_caminhoRaiz = null;
		}

	}
}
