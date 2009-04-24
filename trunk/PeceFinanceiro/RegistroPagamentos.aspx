<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="RegistroPagamentos.aspx.cs" Inherits="PeceFinanceiro.RegistrarPagamentos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemCadastrosFinanceiros");
      
      setInterval("WriteStrings()", 1000);
      
      function WriteStrings()
      {
        var strDatas = "", strValores = "";
        var tableGrid = document.getElementById("ctl00_ContentPlaceHolder1_GridViewParcelas").firstChild;
        var saldo = 0;
        for(var i=1; i < tableGrid.childNodes.length; i++) {
            strDatas += tableGrid.childNodes[i].childNodes[3].firstChild.value + ";";
            strValores += tableGrid.childNodes[i].childNodes[4].firstChild.value + ";";
            
            if(tableGrid.childNodes[i].childNodes[4].firstChild.value != "")
                saldo += moeda2float(tableGrid.childNodes[i].childNodes[4].firstChild.value);
        }
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenFieldDados").value = strDatas + "&" + strValores;
        document.getElementById("TotalRecebido").value = float2moeda(saldo);
      }
      
</script>
    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                <asp:Panel ID="PanelErro" Visible="false" runat="server">
                        <div class="DivErro">
                            <asp:Label ID="MensagemErro" runat="server">Teste</asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelSucesso" Visible="false" runat="server">
                        <div class="DivSucesso">
                            <asp:Label ID="MensagemSucesso" runat="server">Cadastro realizado com sucesso! <a href="ParcelamentoEditar.aspx">Editar Parcelas</a></asp:Label>
                        </div>
                    </asp:Panel>
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Dados do Parcelamento</legend>
                            <ul>
                                <li>
                                    <label>Aluno</label>
                                    <asp:Label runat="server" ID="LabelNomeAluno">João da Silva (44444)</asp:Label>
                                </li>
                                <li>
                                    <label>Projeto</label>
                                    <asp:Label runat="server" ID="LabelProjeto">Projeto</asp:Label>
                                </li>
                                <li>
                                    <label>Valor</label>
                                    <asp:Label runat="server" ID="LabelValor">R$ 4.500,00</asp:Label>
                                    <asp:HiddenField ID="HiddenFieldValorTotal" runat="server" />
                                </li>
                                <li>
                                    <label>Número de Parcelas</label>
                                    <asp:Label runat="server" ID="LabelNumeroParcelas">12</asp:Label>
                                </li>
                            </ul>
                        </fieldset>
                        <fieldset>
                            <legend>Parcelas</legend>
                            <div class="gridParcelas">
                                
                                <asp:GridView ID="GridViewParcelas" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="NumeroParcela" HeaderText="Numero" />
                                        <asp:BoundField DataField="DataVencimento" DataFormatString="{0:d}" 
                                            HeaderText="Vencimento" />
                                        <asp:BoundField DataField="ValorParcela" HeaderText="Valor da Parcela" />
                                        <asp:TemplateField HeaderText="Data do Pagamento">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxVencimento" runat="server" 
                                                    Text='<%# ((DateTime)Eval("DataPagamento")).Equals(DateTime.MinValue)? String.Empty : Eval("DataPagamento", "{0:dd/MM/yyyy}") %>' 
                                                    CssClass="textBoxRight" Width="90%" Enabled='<%# !(bool)Eval("Pago") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor Pago">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" 
                                                    Text='<%# Eval("ValorPago", "{0:#0.00}") %>' CssClass="textBoxRight" 
                                                    Width="90%" Enabled='<%# !(bool)Eval("Pago") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <ul>
                                <li>
                                    <label>Total Recebido</label>
                                    <input type="text" readonly="readonly" name="TotalRecebido" style="color:Green; font-weight:bold; background-color:Transparent; border: 0px solid Transparent;" />
                                </li>
                                <asp:HiddenField ID="HiddenFieldDados" runat="server" />
                            </ul>
                            <asp:Button ID="Button2" runat="server" Text="Salvar" 
                                CssClass="botaoFormTick" onclick="Button2_Click"/>
                            <asp:Button ID="Button3" runat="server" Text="Cancelar" 
                                CssClass="botaoFormCross" onclick="Button3_Click"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
