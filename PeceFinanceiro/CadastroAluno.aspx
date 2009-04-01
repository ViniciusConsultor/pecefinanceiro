<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="CadastroAluno.aspx.cs" Inherits="PeceFinanceiro.CadastroAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemAlunos");
</script>
    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Dados do Aluno</legend>
                            <ul>
                                <li>
                                    <label>Número PECE</label>
                                    <asp:TextBox ID="TextBoxNumeroPece" runat="server" Width="20%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Nome</label>
                                    <asp:TextBox ID="TextBoxNome" runat="server" Width="50%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Projetos</label>
                                    <asp:DropDownList ID="DropDownListProjetos" runat="server" CssClass="textBox">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>1766 aut</asp:ListItem>
                                        <asp:ListItem>1767 seg</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>&nbsp;</label>
                                    <asp:Button ID="ButtonAdicionarProjeto" runat="server" Text="Adicionar" CssClass="botao" />
                                </li>
                                <li>
                                    <label>&nbsp;</label>
                                    <asp:ListBox ID="ListBoxProjetos" runat="server" Rows="5" Width="40%" CssClass="textBox">
                                        <asp:ListItem>1767 seg</asp:ListItem>
                                    </asp:ListBox>
                                    <asp:Button ID="ButtonRemoverProjeto" runat="server" Text="Remover" CssClass="botao" />
                                </li>
                            </ul>
                            <asp:Button ID="ButtonCadastrar" runat="server" Text="Salvar" CssClass="botaoFormTick"/>
                            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="botaoFormCross"/>
                            <asp:Button ID="Button1" runat="server" Text="Limpar Campos" CssClass="botaoForm"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>

</asp:Content>
