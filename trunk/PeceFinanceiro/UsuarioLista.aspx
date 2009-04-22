<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="UsuarioLista.aspx.cs" Inherits="PeceFinanceiro.UsuarioLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemUsuarios");
</script>

    <div id="container">
        <div id="primarycontainer">
            <div id="primarycontent">
                <div class="grid">
                  
                            <div class="grid">
                    <asp:GridView ID="GridViewListaUsuarios" runat="server" 
                        onrowcommand="GridViewListaRegistros_RowCommand" 
                        onrowdeleting="GridViewListaRegistros_RowDeleting" 
                        onrowediting="GridViewListaRegistros_RowEditing" 
                        onselectedindexchanged="GridViewListaRegistros_SelectedIndexChanged">
                    </asp:GridView>
                <br />
                <div>
                    <a href="UsuarioNovoEditar.aspx?operacao=novo" class="botaoAdd">Novo Usuario</a>
                </div>
            </div>
        </div>
        </div>
        </div>

        <div id="secondarycontent">
            <div class="formBuscaDiv">
                    <fieldset>
                        <legend>Busca</legend>
                        <label>Nome: </label><br />
                        <asp:TextBox ID="TextBoxBuscaNomeAluno" runat="server" CssClass="textBox"></asp:TextBox><br />
                        <label>Login: </label><br />
                        <asp:TextBox ID="TextBoxBuscaNumeroPece" runat="server" CssClass="textBox"></asp:TextBox><br />
                        <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    
    </div>

</asp:Content>
