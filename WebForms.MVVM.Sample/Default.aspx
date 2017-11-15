<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms.MVVM.Sample.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Acesso aos objetos</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			Nome:
			<asp:TextBox ID="txtNome" campo-q-eu-quiser="Nome" runat="server"></asp:TextBox><br />

			Idade:
			<asp:TextBox ID="txtIdade" campo-q-eu-quiser="Idade" runat="server"></asp:TextBox><br />

			Endereço:
			<asp:TextBox ID="txtEndereco" campo-q-eu-quiser="Endereco" runat="server"></asp:TextBox><br />

			Estado Civil:
			<asp:RadioButtonList ID="radEstadoCivil" campo-q-eu-quiser="EstadoCivil" runat="server">
				<asp:ListItem Text="Solteiro" Value="S"></asp:ListItem>
				<asp:ListItem Text="Casado" Value="C"></asp:ListItem>
			</asp:RadioButtonList><br />

			Cidade:
			<asp:DropDownList ID="txtCidade" campo-q-eu-quiser="CodigoCidade" runat="server">
				<asp:ListItem Text="Cuiabá-MT" Value="1"></asp:ListItem>
				<asp:ListItem Text="Rondonópolis-MT" Value="2"></asp:ListItem>
				<asp:ListItem Text="Campo Grande-MS" Value="3"></asp:ListItem>
				<asp:ListItem Text="Corumbá-MS" Value="4"></asp:ListItem>
				<asp:ListItem Text="São Paulo-SP" Value="5"></asp:ListItem>
				<asp:ListItem Text="Curitiba-PR" Value="6"></asp:ListItem>
			</asp:DropDownList><br />
			<br />
			<br />
			<br />
			<asp:Button ID="butLer" Text="LER" runat="server" OnClick="butLer_Click" />
			<asp:Button ID="butAtualizar" Text="ATUALIZAR" runat="server" OnClick="butAtualizar_Click" />
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
