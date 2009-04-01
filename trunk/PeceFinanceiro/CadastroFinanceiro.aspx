<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="CadastroFinanceiro.aspx.cs" Inherits="PeceFinanceiro.CadastroFinanceiro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemCadastrosFinanceiros");
</script>
    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Cadastro Financeiro</legend>
                            <ul>
                                <li>
                                    <label>Projeto</label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textBox">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>1766 aut</asp:ListItem>
                                        <asp:ListItem>1767 seg</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Valor do Curso</label>
                                    <asp:Label ID="LabelValorFinal" runat="server">R$ 5.000,00</asp:Label>
                                </li>
                                <li>
                                    <label>Aluno</label>
                                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textBox">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>João da Silva</asp:ListItem>
                                        <asp:ListItem>Sust</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Número de Parcelas</label>
                                    <asp:TextBox ID="TextBoxNumeroParcelas" runat="server" Width="10%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Ajuste no Valor do Curso</label>
                                    <img src="Icons/delete.png" alt="Sinal do ajuste" title="Clique para mudar o sinal."/><asp:TextBox ID="TextBoxAjusteValorFinal" runat="server" Width="15%" CssClass="textBox">R$ 500,00</asp:TextBox>
                                </li>
                                <li>
                                    <label>Valor com Ajuste</label>
                                    <asp:TextBox ID="TextBoxValorComAjuste" runat="server" Width="15%" CssClass="textBox">R$ 4.500,00</asp:TextBox>
                                </li>
                                <li>
                                    <label>Observações</label>
                                    <asp:TextBox ID="TextBoxObservacoes" runat="server" TextMode="MultiLine" CssClass="textBox" Width="50%" Rows="4"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Data da Primeira Parcela</label>
                                    <asp:TextBox ID="TextBoxDataPrimeiraParcela" runat="server" Width="20%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Dia de Pagamento</label>
                                    <asp:TextBox ID="TextBoxDiaPagamento" runat="server" Width="8%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                <br />
                                    <a href="EditarParcelamento.aspx" class="botao">Editar Parcelamento</a>
                                </li>
                            </ul>
                            
                            <asp:Button ID="ButtonCadastrar" runat="server" Text="Salvar" CssClass="botaoFormTick"/>
                            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="botaoFormCross"/>
                            <asp:Button ID="Button1" runat="server" Text="Limpar Campos" CssClass="botaoForm"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>

</asp:Content>
