using System;
using System.Linq.Expressions;

namespace WebForms.MVVM.Framework
{
	public static class ExpressionHelper
	{

		public static string CamihoDaExpressao<T>(Expression<Func<T, object>> expressao)
		{
			if (expressao == null)
				return null;
			string caminho = null;
			EhPossivelConverterExpressaoEmCaminho(expressao.Body, out caminho);
			return caminho;
		}

		public static bool EhPossivelConverterExpressaoEmCaminho(Expression expressao, out string caminho)
		{
			caminho = null;
			var expressaoSemConversao = RemoverConversoesDaExpressao(expressao);
			var member = expressaoSemConversao as MemberExpression;
			var call = expressaoSemConversao as MethodCallExpression;

			if (member != null)
			{
				var caminhoAtual = member.Member.Name;
				string caminhoPai;
				if (!EhPossivelConverterExpressaoEmCaminho(member.Expression, out caminhoPai))
					return false;
				caminho = caminhoPai == null
					? caminhoAtual
					: (caminhoPai + "." + caminhoAtual);
			}
			else if (call != null)
			{
				if (call.Method.Name == "Select"
					&& call.Arguments.Count == 2)
				{
					string caminhoPai;
					if (!EhPossivelConverterExpressaoEmCaminho(call.Arguments[0], out caminhoPai))
						return false;
					if (caminhoPai != null)
					{
						var subExpressao = call.Arguments[1] as LambdaExpression;
						if (subExpressao != null)
						{
							string caminhoAtual;
							if (!EhPossivelConverterExpressaoEmCaminho(subExpressao.Body, out caminhoAtual))
								return false;
							if (caminhoAtual != null)
							{
								caminho = caminhoPai + "." + caminhoAtual;
								return true;
							}
						}
					}
				}
				return false;
			}

			return true;
		}

		private static Expression RemoverConversoesDaExpressao(Expression expressao)
		{
			while (expressao.NodeType == ExpressionType.Convert || expressao.NodeType == ExpressionType.ConvertChecked)
				expressao = ((UnaryExpression)expressao).Operand;
			return expressao;
		}

	}
}
