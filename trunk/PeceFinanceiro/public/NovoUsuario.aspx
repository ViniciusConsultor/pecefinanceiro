<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovoUsuario.aspx.cs" Inherits="PeceFinanceiro.NovoUsuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pece Financeiro - Novo Usuário</title>
    <link rel="stylesheet" type="text/css" href="../../Css/style.css" />
</head>
<body>
     <form id="form1" runat="server">
     
    <div class = "login">
        <div class="formGenerico">
                
        <h1>Pece - Módulo Financeiro</h1>
        <asp:Panel ID="PanelErro" runat="server">
             <div class="DivErro">
                 <asp:Label ID="MensagemErro" runat="server"></asp:Label><br />
                 <br />
                 <br />
                 
              </div>
         </asp:Panel>
         <asp:Panel ID="PanelSucesso" runat="server">
              <div class="DivSucesso">
                  <asp:Label ID="MensagemSucesso" runat="server"></asp:Label>
               </div>
          </asp:Panel>
        <fieldset>
         <legend>Novo Usuário</legend>
        <ul>
        <li>
            <label>Nome</label>
            <asp:TextBox ID="TextBoxNome" runat="server"></asp:TextBox>
        </li>
        <li>
            <label>Login</label>
            <asp:TextBox ID="TextBoxLogin" runat="server" CausesValidation="True"></asp:TextBox>
        </li>
        <li>
            <label>Senha</label>
            <asp:TextBox ID="TextBoxSenha" runat="server" 
                ValidationGroup="senha" TextMode="Password"></asp:TextBox>
        </li>
        <li>
            <label>Confirme a Senha</label>
            <asp:TextBox ID="TextBoxSenhaConfirma" runat="server" ValidationGroup="senha" 
                TextMode="Password"></asp:TextBox>
        </li>
        <li>
            <label>Tipo de Usuário</label>
            <asp:DropDownList ID="DropDownListTipo" runat="server">
            <asp:ListItem Text="Administrador" Value="1"/>
            <asp:ListItem Text="Usuario" Value="0"/>
            </asp:DropDownList>
         </li>
            
         </ul>
        </fieldset>
        <asp:LinkButton ID="LinkButtonCadastrar" runat="server" Text="Salvar" 
                                CssClass="botaoFormTick" onclick="ButtonCadastrar_Click"/>
        <asp:LinkButton ID="LinkButtonCancelar" runat="server" Text="Cancelar" 
                                CssClass="botaoFormCross" onclick="ButtonCancelar_Click"/>
        
    </div>
    </div>
    </form>
</body>
</html>
