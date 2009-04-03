<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="ListaAlunos.aspx.cs" Inherits="PeceFinanceiro.ListaAlunos" Title="Untitled Page" %>
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
                    <table>
                    <tr>
                        <th width="20%">Número Pece</th>
                        <th width="70%">Nome do Aluno</th>
                        <th width="10%"></th>
                    </tr>
                    
                    <tr>
                        
                        <td>44444</td>
                        <td>João da Silva</td>
                        <td>
                            <img src="Icons/page_edit.png" alt="Editar Aluno" title="Editar Aluno"/> <img src="Icons/cross.png" title="Remover Aluno" alt="Remover Aluno"/> 
                            <img src="Icons/money_dollar.png" alt="Cadastro Financeiro" title="Ver Cadastro Financeiro"/></td>
                    </tr>
                    <tr>
                        
                        <td>12345</td>
                        <td>Sust</td>
                        <td>
                            <img src="Icons/page_edit.png"  alt="Editar Aluno" title="Editar Aluno"/> <img src="Icons/cross.png" title="Remover Aluno" alt="Remover Aluno"/>
                            <img src="Icons/money_dollar.png" alt="Cadastro Financeiro" title="Ver Cadastro Financeiro"/></td>
                    </tr>
                    <tr>
                       
                        <td>66666</td>
                        <td>Sustança</td>
                         <td>
                            <img src="Icons/page_edit.png"  alt="Editar Aluno" title="Editar Aluno"/> <img src="Icons/cross.png" title="Remover Aluno" alt="Remover Aluno"/> 
                            <img src="Icons/money_dollar.png" alt="Cadastro Financeiro" title="Ver Cadastro Financeiro"/></td>
                    </tr>
                    <tr>
                        
                        <td>89898</td>
                        <td>Vitor</td>
                        <td>
                            <img src="Icons/page_edit.png"  alt="Editar Aluno" title="Editar Aluno"/> <img src="Icons/cross.png" title="Remover Aluno" alt="Remover Aluno"/>
                            <img src="Icons/money_dollar.png" alt="Cadastro Financeiro" title="Ver Cadastro Financeiro"/></td>
                    </tr>
                    </table>
                </div>
                <br />
                <div>
                    <a href="CadastroAluno.aspx" class="botaoAdd">Novo Aluno</a>
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
