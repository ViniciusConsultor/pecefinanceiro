<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true"
    CodeBehind="UsuarioNovoEditar.aspx.cs" Inherits="PeceFinanceiro.NovoUsuario" EnableEventValidation="false"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if ((bool)Session["isadmin"])
       { %>
    <script type="text/javascript">
        this.setMenuAtivo("MenuItemUsuarios");
    </script>
    <% } %> 
<form action="">
    <div id="container">
        <div id="primarycontainer">
            <div id="primarycontent">
                <asp:Panel ID="PanelErro" runat="server">
                    <div class="DivErro">
                        <asp:Label ID="MensagemErro" runat="server"></asp:Label><br />
                        <br />
                        <br />
                    </div>
                </asp:Panel>
                <div class="formGenerico">
                    <asp:Panel ID="PanelSucesso" runat="server">
                        <div class="DivSucesso">
                            <asp:Label ID="MensagemSucesso" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                    <fieldset>
                        <legend>Novo Usuário</legend>
                        <ul>
                            <li>
                                <label>
                                    Nome</label>
                                <asp:TextBox ID="TextBoxNome" runat="server"> </asp:TextBox>
                            </li>
                            <li>
                                <label>Login</label>
                                <asp:TextBox ID="TextBoxLogin" runat="server" CausesValidation="True" AutoPostBack="False"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Senha</label>
                                <asp:TextBox ID="TextBoxSenha" runat="server" ValidationGroup="senha" TextMode="Password" AutoPostBack="False"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Confirme a Senha</label>
                                <asp:TextBox ID="TextBoxSenhaConfirma" runat="server" ValidationGroup="senha" TextMode="Password" AutoPostBack="False"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Tipo de Usuário</label>
                                <asp:DropDownList ID="DropDownListTipo" runat="server">
                                    <asp:ListItem Text="Administrador" Value="1" />
                                    <asp:ListItem Text="Operacional" Value="0" />
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </fieldset>
                    <asp:LinkButton ID="LinkButtonCadastrar" runat="server" Text="Salvar" CssClass="botaoFormTick"
                        OnClick="ButtonCadastrar_Click" />
                    <asp:LinkButton ID="LinkButtonCancelar" runat="server" Text="Cancelar" CssClass="botaoFormCross"
                        OnClick="ButtonCancelar_Click" />
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
