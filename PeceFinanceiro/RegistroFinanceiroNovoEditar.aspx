<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="RegistroFinanceiroNovoEditar.aspx.cs" Inherits="PeceFinanceiro.CadastroFinanceiro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemCadastrosFinanceiros");
      
      var SinalPostivo = false;
      
      var calendarAtivo = false;
      
      function ToggleCalendar(){
        if(calendarAtivo){
            document.getElementById('DivCalendario').style.display = 'none';
            calendarAtivo = false;
            }
        else {
            document.getElementById('DivCalendario').style.display = '';
            calendarAtivo = true;
            }
      }
      
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
        if(isNaN(valorAjuste))
            valorAjuste = 0.0;
        valorFinal = valorCurso + valorAjuste;
        if(!SinalPostivo){
            valorFinal = valorCurso - valorAjuste;    
            }
        document.getElementById("ctl00$ContentPlaceHolder1$TextBoxValorComAjuste").value = float2moeda(valorFinal);
        document.getElementById("ctl00$ContentPlaceHolder1$HiddenValorComAjuste").value = valorFinal;
            
        nParcelas = parseFloat(document.getElementById("ctl00$ContentPlaceHolder1$TextBoxNumeroParcelas").value);
        valorParcela = valorFinal / nParcelas;
        document.getElementById("ctl00$ContentPlaceHolder1$TextBoxValorParcela").value = float2moeda(valorParcela);
      }
      
</script>
    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                    <asp:Panel ID="PanelErro" runat="server">
                        <div class="DivErro">
                            <asp:Label ID="MensagemErro" runat="server">Teste</asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelSucesso" runat="server">
                        <div class="DivSucesso">
                            Cadastro realizado com sucesso! <a href="EditarParcelamento.aspx">Editar Parcelas</a>
                        </div>
                    </asp:Panel>
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Cadastro Financeiro</legend>
                            <ul>
                                <li>
                                    <label>Projeto</label>
                                    <asp:DropDownList ID="DropDownListProjetos" runat="server" CssClass="textBox" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="DropDownListProjetos_SelectedIndexChanged">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Valor do Curso</label>
                                    <asp:Label ID="LabelValorFinal" runat="server"></asp:Label>
                                    <asp:HiddenField ID="HiddenFieldValorCurso" runat="server" Value="5000,00" />
                                </li>
                                <li>
                                    <label>Aluno</label>
                                     <asp:DropDownList ID="DropDownListAlunos" runat="server" CssClass="textBox" 
                                        onselectedindexchanged="DropDownListAlunos_SelectedIndexChanged">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Número de Parcelas</label>
                                    <asp:TextBox ID="TextBoxNumeroParcelas" runat="server" Width="10%" CssClass="textBox">1</asp:TextBox>
                                </li>
                                <li>
                                    <label>Ajuste no Valor do Curso</label>
                                    <div id="DivSinalPositivo" style="display:none; position:relative; float:left; cursor: pointer;" onclick="javascript:ToggleSinal()" ><img src="Icons/add.png" alt="Sinal do ajuste" title="Clique para mudar o sinal."/></div>
                                    <div id="DivSinalNegativo" style="position:relative; float:left; cursor: pointer;" onclick="javascript:ToggleSinal()"><img src="Icons/delete.png" alt="Sinal do ajuste" title="Clique para mudar o sinal."/></div>
                                    <asp:TextBox ID="TextBoxAjusteValorFinal" runat="server" Width="15%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Valor com Ajuste</label>
                                    <asp:TextBox ID="TextBoxValorComAjuste" runat="server" Width="15%" CssClass="textBox"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenValorComAjuste" runat="server" />
                                </li>
                                <li>
                                    <label>Valor da Parcela</label>
                                    <asp:TextBox ID="TextBoxValorParcela" runat="server" Width="15%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Observações</label>
                                    <asp:TextBox ID="TextBoxObservacoes" runat="server" TextMode="MultiLine" CssClass="textBox" Width="50%" Rows="4"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Data da Primeira Parcela</label>
                                    <asp:TextBox ID="TextBoxDataPrimeiraParcela" runat="server" Width="20%" CssClass="textBox"></asp:TextBox>
                                    <img src="Icons/calender.png" style="cursor:pointer;" alt="Calendar" title="Clique para escolher a data" onclick="javascript:ToggleCalendar();" />
                                    <div id="DivCalendario" style="display:none;" class="Calendario">
                                    <asp:Calendar ID="CalendarDataPrimeiraParcela" runat="server" 
                                            onselectionchanged="CalendarDataPrimeiraParcela_SelectionChanged" 
                                            onvisiblemonthchanged="CalendarDataPrimeiraParcela_VisibleMonthChanged"></asp:Calendar>
                                            <asp:HiddenField ID="CalendarVisibleSignalFromServer" runat="server" Value="" />
                                    </div>
                                </li>
                                <li>
                                    <label>Dia de Pagamento</label>
                                    <asp:TextBox ID="TextBoxDiaPagamento" runat="server" Width="8%" CssClass="textBox"></asp:TextBox>
                                </li>
                                <li>
                                <br />
                                    <asp:Button ID="ButtonEditarParcelas" runat="server" Text="Editar Parcelas" CssClass="botao"/>
                                </li>
                            </ul>
                            
                            <asp:Button ID="ButtonCadastrar" runat="server" Text="Salvar" 
                                CssClass="botaoFormTick" onclick="ButtonCadastrar_Click"/>
                            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" 
                                CssClass="botaoFormCross" onclick="ButtonCancelar_Click"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>

<script type="text/javascript">
    AtualizaValores();
    
    if(document.getElementById("ctl00$ContentPlaceHolder1$CalendarVisibleSignalFromServer").value == "true"){
        ToggleCalendar();
      }
    
</script>

</asp:Content>
