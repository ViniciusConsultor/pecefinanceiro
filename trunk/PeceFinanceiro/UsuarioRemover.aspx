<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="UsuarioRemover.aspx.cs" Inherits="PeceFinanceiro.UsuarioRemover" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemUsuarios");
</script>
    <form id="form1" runat="server">
    <class="DivErro>
        <asp:Label ID="LabelErro" runat="server" Text="Erro"></asp:Label><br />
        
        <asp:HyperLink ID="HyperLinkListaUsuario" runat="server" 
        NavigateUrl="~/UsuarioLista.aspx">Voltar à lista de usuários</asp:HyperLink>
    </div>
    </form>
    
 </asp:Content>

