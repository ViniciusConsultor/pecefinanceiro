﻿<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="ParcelamentoEditar.aspx.cs" Inherits="PeceFinanceiro.EditarParcelamento" Title="Untitled Page" %>
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
            strDatas += tableGrid.childNodes[i].childNodes[1].firstChild.value + ";";
            strValores += tableGrid.childNodes[i].childNodes[2].firstChild.value + ";";
            if(tableGrid.childNodes[i].childNodes[2].firstChild.value != "")
                saldo += moeda2float(tableGrid.childNodes[i].childNodes[2].firstChild.value);
            else
                tableGrid.childNodes[i].childNodes[2].firstChild.value = float2moeda(0.00);
        }
        document.getElementById("ctl00_ContentPlaceHolder1_HiddenFieldDados").value = strDatas + "&" + strValores;
        document.getElementById("SaldoDistribuir").value = float2moeda(moeda2float(document.getElementById("ctl00_ContentPlaceHolder1_HiddenFieldValorTotal").value) - saldo);
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
                                        <asp:TemplateField HeaderText="Vencimento">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBoxVencimento" runat="server" 
                                                    Text='<%# Eval("DataVencimento", "{0:dd/MM/yyyy}") %>' 
                                                    CssClass="textBoxRight" Width="90%" Enabled='<%# !(bool)Eval("Pago") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" 
                                                    Text='<%# Eval("ValorParcela", "{0:#0.00}") %>' CssClass="textBoxRight" 
                                                    Width="90%" Enabled='<%# !(bool)Eval("Pago") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                
                                <!--table>
                                    <tr>
                                        <th width="10%">Parcela</th>
                                        <th width="30%">Valor da Parcela</th>
                                        <th width="30%">Vencimento da Parcela</th>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td><asp:TextBox ID="TextBox1" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBoxData1" runat="server" Width="50%" CssClass="textBox">10/04/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td><asp:TextBox ID="TextBox2" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox3" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td><asp:TextBox ID="TextBox4" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox5" runat="server" Width="50%" CssClass="textBox">10/06/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td><asp:TextBox ID="TextBox6" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox7" runat="server" Width="50%" CssClass="textBox">10/07/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td><asp:TextBox ID="TextBox8" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox9" runat="server" Width="50%" CssClass="textBox">10/08/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>6</td>
                                        <td><asp:TextBox ID="TextBox10" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox11" runat="server" Width="50%" CssClass="textBox">10/09/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>7</td>
                                        <td><asp:TextBox ID="TextBox12" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox13" runat="server" Width="50%" CssClass="textBox">10/10/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>8</td>
                                        <td><asp:TextBox ID="TextBox14" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox15" runat="server" Width="50%" CssClass="textBox">10/11/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>9</td>
                                        <td><asp:TextBox ID="TextBox16" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox17" runat="server" Width="50%" CssClass="textBox">10/12/2009</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>10</td>
                                        <td><asp:TextBox ID="TextBox18" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox19" runat="server" Width="50%" CssClass="textBox">10/01/2010</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>11</td>
                                        <td><asp:TextBox ID="TextBox20" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox21" runat="server" Width="50%" CssClass="textBox">10/02/2010</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>12</td>
                                        <td><asp:TextBox ID="TextBox22" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox23" runat="server" Width="50%" CssClass="textBox">10/03/2010</asp:TextBox></td>
                                    </tr>
                                </table-->
                            </div>
                            <ul>
                                <li>
                                    <label>Saldo a distribuir</label>
                                    <input type="text" readonly="readonly" name="SaldoDistribuir" style="color:Red; font-weight:bold; background-color:Transparent; border: 0px solid Transparent;" />
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
