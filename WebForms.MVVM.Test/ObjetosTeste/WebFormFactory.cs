using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebForms.MVVM.Test.ObjetosTeste
{
	public static class WebFormFactory
	{

		public const string NOMETAG = "campo-model";

		public static Page CriarPaginaDeTestesDoObjetoDeTestes(bool preencherRadios = true)
		{
			var pagina = new Page();

			pagina.Controls.Add(new ComponenteTitulo() { ID = "lblFamiliaItens" }.Tag("CodigoFamiliaItens"));
			pagina.Controls.Add(new ComponentePesquisa() { ID = "txtFamiliaItens" }.Tag("CodigoFamiliaItens"));

			pagina.Controls.Add(new ComponenteTitulo() { ID = "lblNome" }.Tag("Nome"));
			pagina.Controls.Add(new HtmlInputText() { ID = "txtNome" }.Tag("Nome"));

			pagina.Controls.Add(new ComponenteTitulo() { ID = "lblIdade" }.Tag("Idade"));
			pagina.Controls.Add(new HtmlInputText() { ID = "txtIdade" }.Tag("Idade"));

			pagina.Controls.Add(new ComponenteTitulo() { ID = "lblOpcaoNumerica" }.Tag("OpcaoNumerica"));
			pagina.Controls.Add(new RadioButtonList() { ID = "radOpcaoNumerica" }.Tag("OpcaoNumerica"));

			pagina.Controls.Add(new ComponenteTitulo() { ID = "lblOpcaoTexto" }.Tag("OpcaoTexto"));
			pagina.Controls.Add(new RadioButtonList() { ID = "radOpcaoTexto" }.Tag("OpcaoTexto"));

			pagina.Controls.Add(new ComponenteTitulo() { ID = "lblGrupoItens" }.Tag("CodigoGrupoItens"));
			pagina.Controls.Add(new ComponentePesquisa() { ID = "txtGrupoItens" }.Tag("CodigoGrupoItens"));

			pagina.Controls.Add(new DataGrid() { ID = "grdGrupos", AutoGenerateColumns = false }.Tag("GruposItens"));
			var grupos = (DataGrid)pagina.FindControl("grdGrupos");
			ConfigurarGrade(grupos);

			if (preencherRadios)
			{
				var tituloNumerico = (ComponenteTitulo)pagina.FindControl("lblOpcaoNumerica");
				var radioNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");
				PreencherRadioButtons<EnumDeTestes>("Opções Numéricas", tituloNumerico, radioNumerico);

				var tituloTexto = (ComponenteTitulo)pagina.FindControl("lblOpcaoTexto");
				var radioTexto = (RadioButtonList)pagina.FindControl("radOpcaoTexto");
				PreencherRadioButtons<EnumStringDeTestes>("Opções Texto", tituloTexto, radioTexto);
			}

			return pagina;
		}

		public static Control Tag(this UserControl sender, string propriedade)
		{
			sender.Attributes[NOMETAG] = propriedade;
			return sender;
		}

		public static Control Tag(this WebControl sender, string propriedade)
		{
			sender.Attributes[NOMETAG] = propriedade;
			return sender;
		}

		public static Control Tag(this HtmlControl sender, string propriedade)
		{
			sender.Attributes[NOMETAG] = propriedade;
			return sender;
		}

		private static void ConfigurarGrade(DataGrid grupos)
		{
			grupos.Columns.Add(new BoundColumn() { DataField = "CodGrupoItens", HeaderText = "Cod." });
			grupos.Columns.Add(new BoundColumn() { DataField = "NomeGrupoItens", HeaderText = "Nome" });
			grupos.Columns.Add(new BoundColumn() { DataField = "Numero", HeaderText = "Um Número Qualquer" });
			grupos.Columns.Add(CriarColunaTemplateComControleItem());
			grupos.ItemDataBound += Grupos_ItemDataBound;
		}

		private static void PreencherRadioButtons<TEnum>(string texto, ComponenteTitulo titulo, RadioButtonList radio)
		{
			RadioButtonList(texto, titulo, radio, typeof(TEnum));
		}

		public static void RadioButtonList(string textoTitulo, ComponenteTitulo titulo, RadioButtonList controle, Type enumeracao, int indiceSelecionado = 0, RepeatDirection orientacao = RepeatDirection.Horizontal, bool editavel = true, bool autoPostBack = false)
		{
			var listaDeItens = new List<ListItem>();

			foreach (Enum opcao in Enum.GetValues(enumeracao))
			{
				var tituloOpcao = ConsultarAtributo<DescriptionAttribute>(opcao)?.Description 
					?? opcao.ToString();
				var valorOpcao = ConsultarAtributo<DefaultValueAttribute>(opcao)?.Value
					?? opcao.GetHashCode();
				listaDeItens.Add(new ListItem(tituloOpcao, Convert.ToString(valorOpcao)));
			}

			RadioButtonList(textoTitulo, titulo, controle, listaDeItens.ToArray(), indiceSelecionado, orientacao, editavel, autoPostBack);
		}

		public static TAtributo ConsultarAtributo<TAtributo>(Enum opcao)
		{
			var informacoesDaOpcao = opcao.GetType().GetField(opcao.ToString());
			var atributos = informacoesDaOpcao.GetCustomAttributes(typeof(TAtributo), false);
			if (atributos.Length > 0)
				return (TAtributo)atributos[0];
			return default(TAtributo);
		}

		public static void RadioButtonList(string textoTitulo, ComponenteTitulo titulo, RadioButtonList controle, ListItem[] itens, int indiceSelecionado = 0, RepeatDirection orientacao = RepeatDirection.Horizontal, bool editavel = true, bool autoPostBack = false)
		{
			titulo.Texto = textoTitulo;
			foreach (var item in itens)
				controle.Items.Add(item);
			controle.RepeatDirection = orientacao;
			controle.SelectedIndex = indiceSelecionado;
			controle.Enabled = editavel;
			controle.AutoPostBack = autoPostBack;
		}

		public static Page CriarPaginaDeTestesPreenchidaDoObjetoDeTestes(int familiaItens, int quantidadeGrupos, int quantidadeDeAnos)
		{
			var pagina = CriarPaginaDeTestesDoObjetoDeTestes();

			var familia = (ComponentePesquisa)pagina.FindControl("txtFamiliaItens");
			familia.Valor = familiaItens.ToString();

			var idade = (HtmlInputText)pagina.FindControl("txtIdade");
			idade.Value = quantidadeDeAnos.ToString();

			var grupos = (DataGrid)pagina.FindControl("grdGrupos");
			PreencherGrade(grupos, quantidadeGrupos);

			var radioNumerico = (RadioButtonList)pagina.FindControl("radOpcaoNumerica");
			radioNumerico.SelectedValue = "2";

			var radioTexto = (RadioButtonList)pagina.FindControl("radOpcaoTexto");
			radioTexto.SelectedValue = "C";

			return pagina;
		}

		private static void PreencherGrade(DataGrid grupos, int quantidadeGrupos)
		{
			grupos.DataSource = ConsultarGrupos(quantidadeGrupos);
			grupos.DataBind();
		}

		private static TemplateColumn CriarColunaTemplateComControleItem()
		{
			var coluna = new TemplateColumn() { HeaderText = "Item" };
			coluna.ItemTemplate = new ColunaTemplate(ListItemType.Item, "Item");
			return coluna;
		}

		private static void Grupos_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (!(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
				return;

			const int indiceColunaTemplate = 2;
			var registro = (DataRowView)e.Item.DataItem;
			var componente = new ComponentePesquisa() { ID = "txtCodItem" };
			componente.Valor = registro.Row["CodItem"].ToString();
			e.Item.Cells[indiceColunaTemplate].Controls.Add(componente.Tag("GruposItens.CodigoItemPrincipal"));

			sender
				.Should()
				.NotBeNull()
				.And
				.BeOfType<DataGrid>();
		}

		private static DataTable ConsultarGrupos(int quantidadeGrupos)
		{
			var tabela = new DataTable();
			tabela.Columns.Add("CodGrupoItens", typeof(int));
			tabela.Columns.Add("NomeGrupoItens", typeof(string));
			tabela.Columns.Add("Numero", typeof(int));
			tabela.Columns.Add("CodItem", typeof(int));
			for (int grupo = 1; grupo <= quantidadeGrupos; grupo++)
			{
				var registro = tabela.NewRow();
				registro["CodGrupoItens"] = grupo;
				registro["NomeGrupoItens"] = string.Concat("Grupo número ", grupo);
				registro["CodItem"] = grupo * 10;
				if (grupo % 2 == 0)
					registro["Numero"] = grupo * 10;
				tabela.Rows.Add(registro);
			}
			return tabela;
		}

	}
}
