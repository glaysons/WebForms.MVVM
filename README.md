# WebForms.MVVM

WebForms.MVVM é um conjunto de ferramentas que permite tratar os componentes do WebForms de aplicações legadas como **ValueObjects**.

### Porque WebForms.MVVM? ###

 - Cada propriedade do VO pode ser relacionada com um componente WebControl, HtmlControl, TemplateControl, QualquerCoisaControl
 - Os **Testes Automatizados** passam a testar **ValueObjects** e não mais componentes **System.Web.UI**
 - Conversão e atualização de **DataGrid** como um **IList**
 - Nome do atributo de vinculo **ASPX <-> ValueObject** personalizável
 - Você pode preencher uma tela a partir de DataRow/DataTable
 - Transformação automática de enumerações com valores numéricos/texto para os controles
 - Permite o acesso às propriedades de layout de forma a permitir **Testes Automatizados**

## Como Utilizar ##

### Definir os Objetos ###

Todas as telas devem ser representadas como um simples **ValueObject**.

Cada componente do ASPX deve ser relacionado com uma propriedade do **ValueObject**.

  
```
  public class ObjetoDaTela
  {

    public string Nome { get; set; }

    public int? Idade { get; set; }

    public string Endereco { get; set; }

    public int CodigoCidade { get; set; }

    public RegistroCivil EstadoCivil { get; set; }

    [Componente(PropriedadeDePesquisa = "CodigoCidade")]
    public string NomeCidade { get; set; }

  }

  public enum RegistroCivil
  {

    [DefaultValue("S")]
    Solteiro,

    [DefaultValue("C")]
    Casado

  }
```

### Vinculo no ASPX ###

```
  <asp:TextBox ID="txtNome" campo-q-eu-quiser="Nome" runat="server"></asp:TextBox>
  <asp:TextBox ID="txtIdade" campo-q-eu-quiser="Idade" runat="server"></asp:TextBox>
  <asp:TextBox ID="txtEndereco" campo-q-eu-quiser="Endereco" runat="server"></asp:TextBox>
  <asp:RadioButtonList ID="radEstadoCivil" campo-q-eu-quiser="EstadoCivil" runat="server"></asp:RadioButtonList>
  <asp:DropDownList ID="txtCidade" campo-q-eu-quiser="CodigoCidade" runat="server"></asp:DropDownList>
```

### Leitura da Tela ###

```
  var dicionario = new DicionarioTela(MeuWebForm, "campo-q-eu-quiser");
  var tela = new LeitorTela(dicionario).Ler<ObjetoDaTela>();
```

### Atualização da Tela ###

```
  var dicionario = new DicionarioTela(MeuWebForm, "campo-q-eu-quiser");
  var atualizador = new AtualizadorTela(dicionario);

  var conteudo = new ObjetoDaTela()
  {
    Nome = "Abc Da Silva",
    Idade = 15,
    Endereco = "Rua das Flores",
    EstadoCivil = RegistroCivil.Solteiro,
    CodigoCidade = 123
  };

  atualizador.Atualizar(conteudo);
```

### Acesso aos Componentes da Tela ###

```
  var dicionario = new DicionarioTela(MeuWebForm, "campo-q-eu-quiser");
  var layout = new LayoutTela<ObjetoDaTela>(dicionario);

  layout.Componente(c => c.Nome).Editavel = false;
  layout.Componente(c => c.Idade).CssClass = "classe-css";
```
