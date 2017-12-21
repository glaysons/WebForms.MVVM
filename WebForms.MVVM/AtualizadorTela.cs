using System;
using System.Linq.Expressions;
using System.Reflection;
using WebForms.MVVM.Attributes;
using WebForms.MVVM.Atualizadores;
using WebForms.MVVM.Framework;

namespace WebForms.MVVM
{
	public class AtualizadorTela
	{

		private DicionarioTela _dicionario;
		private AtualizadorGrade _atualizadorGrade;
		private AtualizadorEnum _atualizadorEnum;

		public AtualizadorTela(DicionarioTela dicionario)
		{
			_dicionario = dicionario;
			if (_dicionario == null)
				throw new Exception("Favor informar um dicionario válido!");

			_atualizadorGrade = new AtualizadorGrade(_dicionario);
			_atualizadorEnum = new AtualizadorEnum(_dicionario);
		}

		public void Atualizar<TCamposDaTela>(TCamposDaTela objeto)
		{
			AtualizarTela(typeof(TCamposDaTela), objeto);
		}

		private void AtualizarTela(Type tipoDoObjeto, object objeto)
		{
			foreach (PropertyInfo propriedade in tipoDoObjeto.GetProperties())
			{
				var componente = Consultador.ConsultarConfiguracaoDaPropriedade(propriedade);
				if (componente == null)
					continue;

				if (Comparador.EhUmaListaGenerica(propriedade.PropertyType))
					_atualizadorGrade.AtualizarComponenteGrade(objeto, propriedade);

				else if (Comparador.EhUmEnum(propriedade.PropertyType))
					_atualizadorEnum.PreencherPropriedadeEnum(objeto, propriedade, componente);

				else
					AtualizarComponente(objeto, propriedade, componente);
			}
		}

		private void AtualizarComponente(object objeto, PropertyInfo propriedade, ComponenteAttribute componente)
		{
			var valor = (objeto == null)
				? null
				: propriedade.GetValue(objeto, null);
			AtualizadorObjetos.Atualizar(propriedade.Name, _dicionario, componente, valor);
		}

		public void Atualizar<TCamposDaTela, TSubCamposDaTela>(Expression<Func<TCamposDaTela, object>> raiz, TSubCamposDaTela objeto)
		{
			var caminho = ExpressionHelper.CamihoDaExpressao(raiz);
			_dicionario.AtivarCaminhoRaiz(caminho);
			try
			{
				Atualizar(objeto);
			}
			finally
			{
				_dicionario.DesativarCaminhoRaiz();
			}
		}

		public void Limpar<TCamposDaTela>()
		{
			AtualizarTela(typeof(TCamposDaTela), null);
		}

		public void Limpar<TCamposDaTela, TSubCamposDaTela>(Expression<Func<TCamposDaTela, object>> raiz)
		{
			var caminho = ExpressionHelper.CamihoDaExpressao(raiz);
			_dicionario.AtivarCaminhoRaiz(caminho);
			try
			{
				AtualizarTela(typeof(TSubCamposDaTela), null);
			}
			finally
			{
				_dicionario.DesativarCaminhoRaiz();
			}
		}

	}
}
