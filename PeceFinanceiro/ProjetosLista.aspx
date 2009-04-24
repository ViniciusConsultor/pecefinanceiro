<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="ProjetosLista.aspx.cs" Inherits="PeceFinanceiro.ListaProjetos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Lista de Projetos - PECE Financeiro</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    var editando = "false";
    
      this.setMenuAtivo("MenuItemProjetos");
</script>

    <div id="container">
        <div id="primarycontainer">
            <div id="primarycontent">
            
                <div class="grid">
                    <asp:GridView ID="GridViewListaProjetos" runat="server" 
                        onrowcommand="GridViewListaProjetos_RowCommand" 
                        onrowdeleting="GridViewListaProjetos_RowDeleting" 
                        onrowediting="GridViewListaProjetos_RowEditing" 
                        onselectedindexchanged="GridViewListaProjetos_SelectedIndexChanged">
                    </asp:GridView>
                    <!--table>
                    <tr>
                        <th>Código</th>
                        <th>Nome do Projeto</th>
                        <th>Preço Total</th>
                        <th></th>
                    </tr>
                    
                    <tr>
                        <td>4444</td>
                        <td>Automação Industrial</td>
                        <td>R$ 5.000,00</td>
                        <td><img src="Icons/page_edit.png" alt="Editar Projeto" title="Editar Projeto"/><img src="Icons/cross.png"  title="Remover Projeto"/></td>
                    </tr>
                    <tr>
                        <td>5555</td>
                        <td>Sust</td>
                        <td>R$ 20.000,00</td>
                        <td><img src="Icons/page_edit.png" alt="Editar Projeto" title="Editar Projeto"/><img src="Icons/cross.png"  title="Remover Projeto" /></td>
                    </tr>
                    <tr>
                        <td>6666</td>
                        <td>Informática</td>
                        <td>R$ 2.000,00</td>
                        <td><img src="Icons/page_edit.png" alt="Editar Projeto" title="Editar Projeto"/><img src="Icons/cross.png"  title="Remover Projeto" /></td>
                    </tr>
                    <tr>
                        <td>7777</td>
                        <td>Cozinha Industrial</td>
                        <td>R$ 30.000,00</td>
                        <td><img src="Icons/page_edit.png" alt="Editar Projeto" title="Editar Projeto"/><img src="Icons/cross.png"   title="Remover Projeto"/></td>
                    </tr>
                    </table-->
                </div>
                <br />
                <div>
                    <a href="ProjetoNovoEditar.aspx" class="botaoAdd">Novo Projeto</a>
                </div>
            </div>
        </div>

        <div id="secondarycontent">
        
                <div class="formBuscaDiv">
                    <fieldset>
                        <legend>Busca</legend>
                            <label>Palavra Chave: </label><br />
                            <asp:TextBox ID="TextBoxBuscaNomeProjeto" class="textBox" runat="server"></asp:TextBox>
                            <br />
                            <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    </div>

</asp:Content>
