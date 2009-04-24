<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="RegistroFinanceiroLista.aspx.cs" Inherits="PeceFinanceiro.ListaCadastros" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Lista de Cadastro Financeiro - PECE Financeiro</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemCadastrosFinanceiros");
</script>
    <div id="container">
        <div id="primarycontainer">
            <div id="primarycontent">
            
                
                <div class="grid">
                    <asp:GridView ID="GridViewListaRegistros" runat="server" 
                        onrowcommand="GridViewListaRegistros_RowCommand" 
                        onrowdeleting="GridViewListaRegistros_RowDeleting" 
                        onrowediting="GridViewListaRegistros_RowEditing" 
                        onselectedindexchanged="GridViewListaRegistros_SelectedIndexChanged">
                    </asp:GridView>
                </div>
                <br />
                <div>
                    <a href="RegistroFinanceiroNovoEditar.aspx" class="botaoAdd">Novo Cadastro</a>
                </div>
            </div>
        </div>

        <div id="secondarycontent">
            <div class="formBuscaDiv">
                    <fieldset>
                        <legend>Busca</legend>
                        <label>Nome do Aluno</label><br /><asp:TextBox ID="TextBoxBuscaNomeAluno" runat="server" CssClass="textBox"></asp:TextBox>
                        <label>Projeto</label> <br />
                        <asp:TextBox ID="TextBoxProjeto" runat="server" CssClass="textBox"></asp:TextBox>
                        <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao" 
                            onclick="ButtonBuscar_Click"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    </div>

</asp:Content>
