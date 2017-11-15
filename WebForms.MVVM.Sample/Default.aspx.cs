using System;
using System.Text;

namespace WebForms.MVVM.Sample
{
	public partial class Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void butLer_Click(object sender, EventArgs e)
		{
			var dicionario = new DicionarioTela(this, "campo-q-eu-quiser");
			var tela = new LeitorTela(dicionario).Ler<ObjetoDaTela>();

			var conteudo = new StringBuilder();

			conteudo.Append("Nome: ");
			conteudo.AppendLine(tela.Nome);

			conteudo.Append("Idade: ");
			conteudo.AppendLine(tela.Idade?.ToString() ?? string.Empty);

			conteudo.Append("Endereço: ");
			conteudo.AppendLine(tela.Endereco);

			conteudo.Append("Estado Civil: ");
			conteudo.AppendLine(tela.EstadoCivil.ToString());

			conteudo.Append("Código da Cidade: ");
			conteudo.AppendLine(tela.CodigoCidade.ToString());

			conteudo.Append("Nome da Cidade: ");
			conteudo.AppendLine(tela.NomeCidade);

			lblConteudo.Text = conteudo.ToString();
		}

		protected void butAtualizar_Click(object sender, EventArgs e)
		{
			var dicionario = new DicionarioTela(this, "campo-q-eu-quiser");
			var atualizador = new AtualizadorTela(dicionario);

			var conteudo = new ObjetoDaTela()
			{
				Nome = "Abc Da Silva",
				Idade = 25,
				Endereco = "Rua das Flores",
				EstadoCivil = RegistroCivil.Casado,
				CodigoCidade = 4
			};

			atualizador.Atualizar(conteudo);
		}
	}
}