﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms.MVVM.Sample.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Acesso aos objetos</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			Nome:
			<asp:TextBox campo-q-eu-quiser="Nome" runat="server"></asp:TextBox><br />

			Idade:
			<asp:TextBox campo-q-eu-quiser="Idade" runat="server"></asp:TextBox><br />

			Endereço:
			<asp:TextBox campo-q-eu-quiser="Endereco" runat="server"></asp:TextBox><br />

			Estado Civil:
			<asp:RadioButtonList campo-q-eu-quiser="EstadoCivil" runat="server">
				<asp:ListItem Text="Solteiro" Value="S"></asp:ListItem>
				<asp:ListItem Text="Casado" Value="C"></asp:ListItem>
			</asp:RadioButtonList><br />

			Cidade:
			<asp:DropDownList campo-q-eu-quiser="CodigoCidade" runat="server">
				<asp:ListItem Text="Cuiabá-MT" Value="1"></asp:ListItem>
				<asp:ListItem Text="Rondonópolis-MT" Value="2"></asp:ListItem>
				<asp:ListItem Text="Campo Grande-MS" Value="3"></asp:ListItem>
				<asp:ListItem Text="Corumbá-MS" Value="4"></asp:ListItem>
				<asp:ListItem Text="São Paulo-SP" Value="5"></asp:ListItem>
				<asp:ListItem Text="Curitiba-PR" Value="6"></asp:ListItem>
			</asp:DropDownList><br />
			<br />
			<br />

			<hr />

			<br />
			Filhos:<br />
			Nome:
			<asp:TextBox campo-q-eu-quiser="Filhos.Nome" runat="server"></asp:TextBox><br />

			Idade:
			<asp:TextBox campo-q-eu-quiser="Filhos.Idade" runat="server"></asp:TextBox><br />

			Data de Nascimento: 
			<asp:TextBox campo-q-eu-quiser="Filhos.DataDeNascimento" runat="server"></asp:TextBox><br />
			<br />
			<br />
			<asp:Button runat="server" Text="Salvar" OnClick="butSalvarFilho_Click" />
			<asp:Button runat="server" Text="Cancelar" OnClick="butCancelarFilho_Click" />
			<br />
			<br />
			<asp:DataGrid ID="gradeFilhos" campo-q-eu-quiser="Filhos" runat="server" OnItemCommand="gradeFilhos_ItemCommand" AutoGenerateColumns="false">
				<Columns>
					<asp:ButtonColumn Text="Alterar" CommandName="Alterar"></asp:ButtonColumn>
					<asp:ButtonColumn Text="Excluir" CommandName="Excluir"></asp:ButtonColumn>
					<asp:BoundColumn HeaderText="Nome" DataField="Nome"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Idade" DataField="Idade"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Data de Nascimento" DataField="DataDeNascimento"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid> <br />
			<br />
			<br />
			<br />

			<hr />

			<br />
			<br />
			<br />
			<asp:Button Text="LER" runat="server" OnClick="butLer_Click" />
			<asp:Button Text="ATUALIZAR" runat="server" OnClick="butAtualizar_Click" />
			<br />
			<br />
			<br />
			Conteudo:
			<pre>
<asp:Label ID="lblConteudo" Text="" runat="server"></asp:Label>
			</pre>
		</div>
	</form>
</body>
</html>
