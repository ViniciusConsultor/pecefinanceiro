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
                <asp:Panel ID="PanelErro" runat="server">
                     <div class="DivErro">
                            <asp:Label ID="MensagemErro" runat="server"></asp:Label><br />
                            
                       </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelSucesso" runat="server">
                        <div class="DivSucesso">
                            <asp:Label ID="MensagemSucesso" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                <div class="grid">
                    <asp:GridView ID="GridViewListaProjetos" runat="server" 
                        onrowdeleting="GridViewListaProjetos_RowDeleting" 
                        onrowediting="GridViewListaProjetos_RowEditing">
                    </asp:GridView>
                    
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
                            <label>Nome do Projeto: </label><br />
                            <asp:TextBox ID="TextBoxBuscaNomeProjeto" class="textBox" runat="server"></asp:TextBox>
                            <br />
                            <label>Código do Projeto: </label><br />
                            <asp:TextBox ID="TextBoxBuscaCodigoProjeto" runat="server" CssClass="textBox"></asp:TextBox><br />
                            <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao" 
                            onclick="ButtonBuscar_Click"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    </div>

</asp:Content>
