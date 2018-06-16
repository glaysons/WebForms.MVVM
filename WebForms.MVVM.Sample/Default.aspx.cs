using System;
using System.Collections.Generic;
using System.Text;

namespace WebForms.MVVM.Sample
{
	public partial class Default : System.Web.UI.Page
	{

		private DicionarioTela _dicionario = null;
		private ObjetoDaTela _telaAtual = null;
		private LeitorTela _leitor = null;
		private AtualizadorTela _atualizador = null;

		private DicionarioTela Dicionario
		{
			get { return _dicionario ?? (_dicionario = new DicionarioTela(this, "campo-q-eu-quiser")); }
		}

		private LeitorTela Leitor
		{
			get { return _leitor ?? (_leitor = new LeitorTela(Dicionario)); }
		}

		private AtualizadorTela Atualizador
		{
			get { return _atualizador ?? (_atualizador = new AtualizadorTela(Dicionario)); }
		}

		private ObjetoDaTela TelaAtual
		{
			get { return _telaAtual ?? (_telaAtual = Leitor.Ler<ObjetoDaTela>()); }
		}


		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void butLer_Click(object sender, EventArgs e)
		{
			var conteudo = new StringBuilder();

			conteudo.Append("Nome: ");
			conteudo.AppendLine(TelaAtual.Nome);

			conteudo.Append("Idade: ");
			conteudo.AppendLine(TelaAtual.Idade?.ToString() ?? string.Empty);

			conteudo.Append("Endereço: ");
			conteudo.AppendLine(TelaAtual.Endereco);

			conteudo.Append("Estado Civil: ");
			conteudo.AppendLine(TelaAtual.EstadoCivil.ToString());

			conteudo.Append("Código da Cidade: ");
			conteudo.AppendLine(TelaAtual.CodigoCidade.ToString());

			conteudo.Append("Nome da Cidade: ");
			conteudo.AppendLine(TelaAtual.NomeCidade);

			conteudo.AppendLine("Filhos: Nome / Idade / Data de Nascimento");
			var numero = 0;
			foreach (var filho in TelaAtual.Filhos)
			{
				numero++;
				conteudo.Append(" > ");
				conteudo.Append(numero);
				conteudo.Append(": ");
				conteudo.Append(filho.Nome);
				conteudo.Append(" / ");
				conteudo.Append(filho.Idade);
				conteudo.Append(" / ");
				conteudo.Append(filho.DataDeNascimento);
				conteudo.AppendLine();
			}

			lblConteudo.Text = conteudo.ToString();
		}

		protected void butAtualizar_Click(object sender, EventArgs e)
		{
			var conteudo = new ObjetoDaTela()
			{
				Nome = "Abc Da Silva",
				Idade = 25,
				Endereco = "Rua das Flores",
				EstadoCivil = RegistroCivil.Casado,
				CodigoCidade = 4,
				Filhos = new List<ItemDaTela>()
				{
					new ItemDaTela()
					{
						Nome = "Abczinho Da Silva",
						Idade = 5,
						DataDeNascimento = new DateTime(2013, 6, 2)
					},
					
					new ItemDaTela()
					{
						Nome = "Abczinha Da Silva",
						Idade = 2,
						DataDeNascimento = new DateTime(2016, 6, 1)
					}

				}
			};

			Atualizador.Atualizar(conteudo);
		}

		protected void butSalvarFilho_Click(object sender, EventArgs e)
		{
			var filho = (ItemDaTela)Leitor.Ler<ObjetoDaTela>(o => o.Filhos);

			TelaAtual.Filhos.Add(filho);

			Atualizador.Atualizar(TelaAtual);

			LimparTelaPreenchimentoDosFilhos();
		}

		protected void butCancelarFilho_Click(object sender, EventArgs e)
		{
			LimparTelaPreenchimentoDosFilhos();
		}

		private void LimparTelaPreenchimentoDosFilhos()
		{
			Atualizador.Limpar<ObjetoDaTela>(o => o.Filhos);
		}

		protected void gradeFilhos_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Alterar")
				Atualizador.Atualizar<ObjetoDaTela>(o => o.Filhos, TelaAtual.Filhos[e.Item.ItemIndex]);

			else if (e.CommandName == "Excluir")
			{
				TelaAtual.Filhos.RemoveAt(e.Item.ItemIndex);
				Atualizador.Atualizar(TelaAtual);
			}
		}
	}
}