<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Login.aspx.cs" Inherits="PeceFinanceiro.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PeceFinanceiro Login</title>
    <link rel="stylesheet" type="text/css" href="../../Css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="login">
        <h1>Pece - Módulo Financeiro</h1>
        <asp:Login ID="LoginFinanceiro" runat="server" style="text-align: center" 
            TitleText="" DestinationPageUrl="~/RegistroFinanceiroLista.aspx" 
            FailureText="Não foi possível efetuar o login. Verifique o usuário e senha digitados" 
            onauthenticate="LoginFinanceiro_Authenticate" PasswordLabelText="Senha:" 
            PasswordRequiredErrorMessage="Digite a Senha." UserNameLabelText="Usuário:" 
            UserNameRequiredErrorMessage="Digite o Usuário.">
        </asp:Login>
    </div>
    </form>
    
    <div id="footer">
        &copy; 2009 Pece. Design by WALF.
    </div>
</body>
</html>
