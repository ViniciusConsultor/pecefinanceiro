<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="AlunoRemover.aspx.cs" Inherits="PeceFinanceiro.AlunoRemover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemAlunos");
</script>
    
    <class="DivErro>
        <asp:Label ID="LabelErro" runat="server" Text="Erro"></asp:Label><br />
        
        <asp:HyperLink ID="HyperLinkListaAlunos" runat="server" 
        NavigateUrl="~/UsuarioLista.aspx">Voltar à lista de alunos</asp:HyperLink>
    </div>
    
    
 </asp:Content>