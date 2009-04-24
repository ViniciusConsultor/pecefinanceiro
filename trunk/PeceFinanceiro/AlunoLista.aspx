<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="AlunoLista.aspx.cs" Inherits="PeceFinanceiro.ListaAlunos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemAlunos");
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
                            <asp:Label ID="MensagemSucesso" runat="server">Cadastro realizado com sucesso! <a href="ParcelamentoEditar.aspx">Editar Parcelas</a></asp:Label>
                        </div>
                    </asp:Panel>
                <div class="grid">
                  
                        <asp:GridView ID="GridViewListaAlunos" runat="server" 
                        onrowcommand="GridViewListaAlunos_RowCommand" 
                        onrowdeleting="GridViewListaAlunos_RowDeleting" 
                        onrowediting="GridViewListaAlunos_RowEditing" 
                        onselectedindexchanged="GridViewListaAlunos_SelectedIndexChanged">
                    </asp:GridView>
                            </div>
                            <br />
                <div>
                    <a href="AlunoNovoEditar.aspx" class="botaoAdd">Novo Aluno</a>
                </div>
            </div>
        </div>

        <div id="secondarycontent">
            <div class="formBuscaDiv">
                    <fieldset>
                        <legend>Busca</legend>
                        <label>Nome: </label><br />
                        <asp:TextBox ID="TextBoxBuscaNomeAluno" runat="server" CssClass="textBox"></asp:TextBox><br />
                        <label>Numero Pece: </label><br />
                        <asp:TextBox ID="TextBoxBuscaNumeroPece" runat="server" CssClass="textBox"></asp:TextBox><br />
                        <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao" 
                            onclick="ButtonBuscar_Click"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    </div>

</asp:Content>
