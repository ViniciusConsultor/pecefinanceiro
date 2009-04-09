<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="RegistroPagamentos.aspx.cs" Inherits="PeceFinanceiro.RegistrarPagamentos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemRegistrarPagamentos");
</script>
<div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Dados do Parcelamento</legend>
                            <ul>
                                <li>
                                    <label>Aluno</label>
                                    <asp:Label runat="server" ID="Label4">João da Silva (44444)</asp:Label>
                                </li>
                                <li>
                                    <label>Valor do Projeto</label>
                                    <asp:Label runat="server" ID="LabelValorCurso">R$ 5.000,00</asp:Label>
                                </li>
                                <li>
                                    <label>Valor do Ajuste</label>
                                    <asp:Label runat="server" ID="Label1">- R$ 500,00</asp:Label>
                                </li>
                                <li>
                                    <label>Valor Final</label>
                                    <asp:Label runat="server" ID="Label2">R$ 4.500,00</asp:Label>
                                </li>
                                <li>
                                    <label>Número de Parcelas</label>
                                    <asp:Label runat="server" ID="Label6">12</asp:Label>
                                </li>
                            </ul>
                        </fieldset>
                        <fieldset>
                            <legend>Parcelas</legend>
                            <div class="grid">
                                <table>
                                    <tr>
                                        <th width="10%">Parcela</th>
                                        <th width="30%">Valor da Parcela</th>
                                        <th width="30%">Vencimento da Parcela</th>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td><asp:Label ID="Label30" runat="server" Width="50%" CssClass="textBox">R$ 375</asp:Label></td>
                                        <td><asp:Label ID="LabelData1" runat="server" Width="50%" CssClass="textBox">10/04/2009</asp:Label></td>
                                        <td>R$ <asp:TextBox ID="TextBoxValorPagamento" runat="server" Width="50%" CssClass="textBox">375</asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td><asp:Label ID="Label5" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label7" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                        <td><asp:Button ID="ButtonPagar"  /></td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td><asp:Label ID="Label8" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label9" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td><asp:Label ID="Label10" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label11" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td><asp:Label ID="Label12" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label13" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>6</td>
                                        <td><asp:Label ID="Label14" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label15" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>7</td>
                                        <td><asp:Label ID="Label16" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label17" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>8</td>
                                        <td><asp:Label ID="Label18" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label19" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>9</td>
                                        <td><asp:Label ID="Label20" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label21" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>10</td>
                                        <td><asp:Label ID="Label22" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label23" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>11</td>
                                        <td><asp:Label ID="Label24" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label25" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>12</td>
                                        <td><asp:Label ID="Label26" runat="server" Width="50%" CssClass="textBox">R$ 275</asp:Label></td>
                                        <td><asp:Label ID="Label27" runat="server" Width="50%" CssClass="textBox">10/05/2009</asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            <ul>
                                <li>
                                    <label>Saldo a pagar</label>
                                    <asp:Label runat="server" ID="Label3" ForeColor="Red" Font-Bold="true" Font-Size="14px">R$ 100,00</asp:Label>
                                </li>
                            </ul>
                            <asp:Button ID="Button2" runat="server" Text="Salvar" Enabled="false" CssClass="botaoFormTick"/>
                            <asp:Button ID="Button3" runat="server" Text="Cancelar" CssClass="botaoFormCross"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>

</asp:Content>
