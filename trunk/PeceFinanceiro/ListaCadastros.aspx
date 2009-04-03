<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="ListaCadastros.aspx.cs" Inherits="PeceFinanceiro.ListaCadastros" Title="Untitled Page" %>
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
                    <table>
                    <tr>
                        <th>Nome do Aluno</th>
                        <th>Projeto</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                    
                    <tr>
                        <td>João da Silva</td>
                        <td>Segurança do Trabalho</td>
                        <td class="Inadimplente">Inadimplente</td>
                        <td>
                        <img src="Icons/page_edit.png" alt="Editar Cadastro" title="Editar Cadastro" /> 
                            <img src="Icons/money_add.png" alt="Registrar Pagamento" title="Registrar Pagamento"/></td>
                    </tr>
                    <tr>
                        <td>Sust</td>
                        <td>Automação Industrial</td>
                        <td class="EmDia">Em dia</td>
                        <td>
                        <img src="Icons/page_edit.png" alt="Editar Cadastro" title="Editar Cadastro" /> 
                            <img src="Icons/money_add.png" alt="Registrar Pagamento" title="Registrar Pagamento"/></td>
                    </tr>
                    <tr>
                        <td>Sustança</td>
                        <td>Automação Industrial</td>
                        <td class="Inadimplente">Inadimplente</td>
                        <td>
                            <img src="Icons/page_edit.png"  alt="Editar Cadastro" title="Editar Cadastro"/> 
                            <img src="Icons/money_add.png" alt="Registrar Pagamento" title="Registrar Pagamento" /></td>
                    </tr>
                    <tr>
                        <td>Vitor</td>
                        <td>Automação Industrial</td>
                        <td class="EmDia">Em dia</td>
                        <td>
                        <img src="Icons/page_edit.png" alt="Editar Cadastro" title="Editar Cadastro" /> 
                            <img src="Icons/money_add.png" alt="Registrar Pagamento" title="Registrar Pagamento"/></td>
                    </tr>
                    </table>
                </div>
                <br />
                <div>
                    <a href="CadastroFinanceiro.aspx" class="botaoAdd">Novo Cadastro</a>
                </div>
            </div>
        </div>

        <div id="secondarycontent">
            <div class="formBuscaDiv">
                    <fieldset>
                        <legend>Busca</legend>
                        <label>Nome do Aluno</label><br /><asp:TextBox ID="TextBoxBuscaNomeAluno" runat="server" CssClass="textBox"></asp:TextBox>
                        <label>Projeto</label> <br />
                        <asp:DropDownList ID="DropDownListBuscaProjeto" runat="server" CssClass="textBox">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem>1766 aut</asp:ListItem>
                            <asp:ListItem>1767 seg</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <label>Status</label> <br />
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textBox">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem>Em dia</asp:ListItem>
                            <asp:ListItem>Inadimplente</asp:ListItem>
                            <asp:ListItem>Inativo</asp:ListItem>
                            <asp:ListItem>Removido</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="botao"/>
                    </fieldset>
                </div>
            <br />
        </div>

        <div class="clearit"></div>

    </div>

</asp:Content>
