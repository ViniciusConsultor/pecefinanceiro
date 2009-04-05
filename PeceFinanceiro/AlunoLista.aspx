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
                <div class="grid">
                  
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="ListaTodosAlunos">
                                <Columns>
                                    <asp:BoundField DataField="NumeroPece" HeaderText="NumeroPece" 
                                        SortExpression="NumeroPece" />
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                                    <asp:HyperLinkField DataNavigateUrlFields="NumeroPece" 
                                        DataNavigateUrlFormatString="AlunoNovoEditar.aspx?operacao=editar&amp;IdAluno={0}" 
                                        DataTextFormatString="Edit" 
                                        Text="&lt;img src='Icons/page_edit.png' border='0' alt='Excluir'" />
                                    <asp:CommandField ShowDeleteButton="True" 
                                        DeleteText="&lt;img src='Icons/cross.png' border='0' alt='Excluir'" 
                                        EditImageUrl="~/Icons/page_edit.png" />
                                    <asp:HyperLinkField DataNavigateUrlFields="NumeroPece" 
                                        DataNavigateUrlFormatString="RegistroFinanceiroLista.aspx?operacao=Busca&amp;NumeroPece={0}" 
                                        DataTextFormatString="$$$$" 
                                        Text="&lt;img src='Icons/Money_dollar.png' border='0' alt='Excluir'" />
                                </Columns>
                            </asp:GridView>
                            </div>
                            <asp:ObjectDataSource ID="ListaTodosAlunos" runat="server" 
                                SelectMethod="ObterTodosAlunos" TypeName="Vsf.Negocio.AlunoNegocio">
                            </asp:ObjectDataSource>
                <br />
                <div>
                    <a href="AlunoNovoEditar.aspx?operacao=novo" class="botaoAdd">Novo Aluno</a>
                </div>
            </div>
        </div>

        <div id="secondarycontent">
            <div class="formBuscaDiv">
                    <fieldset>
                        <legend>Busca</legend>
                        <label>Palavra Chave: </label><br />
                        <asp:TextBox ID="TextBoxBuscaNomeAluno" runat="server" CssClass="textBox"></asp:TextBox>
                        <br />
                        <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    </div>

</asp:Content>
