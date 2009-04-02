<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="CadastroFinanceiro.aspx.cs" Inherits="PeceFinanceiro.CadastroFinanceiro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemCadastrosFinanceiros");
      
      var SinalPostivo = false;
      
      function ToggleSinal(){
        if(SinalPostivo){
            document.getElementById("DivSinalPositivo").style.display = "none";
            document.getElementById("DivSinalNegativo").style.display = "";
            SinalPostivo = false;
        } else {
            document.getElementById("DivSinalPositivo").style.display = "";
            document.getElementById("DivSinalNegativo").style.display = "none";
            SinalPostivo = true;
        }
        AtualizaValores();
      }
      
      function AtualizaValores(){
        var valorAjuste = moeda2float(document.getElementById("ctl00$ContentPlaceHolder1$TextBoxAjusteValorFinal").value);
        var valorCurso = moeda2float(document.getElementById("ctl00$ContentPlaceHolder1$HiddenFieldValorCurso").value);
        var valorFinal = 0.0;
        var nParcelas = 0.0;
        var valorParcela = 0.0;
        if(SinalPostivo){
            valorFinal = valorCurso + valorAjuste;
            document.getElementById("ctl00$ContentPlaceHolder1$TextBoxValorComAjuste").value = float2moeda(valorFinal);
            }
        else{
            valorFinal = valorCurso - valorAjuste;
            document.getElementById("ctl00$ContentPlaceHolder1$TextBoxValorComAjuste").value = float2moeda(valorFinal);
            }
            
        nParcelas = parseFloat(document.getElementById("ctl00$ContentPlaceHolder1$TextBoxNumeroParcelas").value);
        valorParcela = valorFinal / nParcelas;
        document.getElementById("ctl00$ContentPlaceHolder1$TextBoxValorParcela").value = float2moeda(valorParcela);
      }
      
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
                                    <asp:DropDownList ID="DropDownListProjetos" runat="server" CssClass="textBox">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>1766 aut</asp:ListItem>
                                        <asp:ListItem>1767 seg</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Valor do Curso</label>
                                    <asp:Label ID="LabelValorFinal" runat="server">5.000,00</asp:Label>
                                    <asp:HiddenField ID="HiddenFieldValorCurso" runat="server" Value="5000,00" />
                                </li>
                                <li>
                                    <label>Aluno</label>
                                     <asp:DropDownList ID="DropDownListAlunos" runat="server" CssClass="textBox">
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
                                    <div id="DivSinalPositivo" style="display:none; position:relative; float:left; cursor: pointer;" onclick="javascript:ToggleSinal()" ><img src="Icons/add.png" alt="Sinal do ajuste" title="Clique para mudar o sinal."/></div>
                                    <div id="DivSinalNegativo" style="position:relative; float:left; cursor: pointer;" onclick="javascript:ToggleSinal()"><img src="Icons/delete.png" alt="Sinal do ajuste" title="Clique para mudar o sinal."/></div>
                                    <asp:TextBox ID="TextBoxAjusteValorFinal" runat="server" Width="15%" CssClass="textBox">500,00</asp:TextBox>
                                </li>
                                <li>
                                    <label>Valor com Ajuste</label>
                                    <asp:TextBox ID="TextBoxValorComAjuste" runat="server" Width="15%" CssClass="textBox">4.500,00</asp:TextBox>
                                </li>
                                <li>
                                    <label>Valor da Parcela</label>
                                    <asp:TextBox ID="TextBoxValorParcela" runat="server" Width="15%" CssClass="textBox">0,00</asp:TextBox>
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
