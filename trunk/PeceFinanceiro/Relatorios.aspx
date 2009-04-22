<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="Relatorios.aspx.cs" Inherits="PeceFinanceiro.Relatorios" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemRelatorios");
</script>
    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Cadastro Financeiro</legend>
                    <asp:Panel ID="PanelSucesso" runat="server" Visible="False">
                        <div class="DivMensagem">
                            Não foram encontrados registros para o mês selecionado 
                        </div>
                    </asp:Panel>
                            <ul>
                                <li>
                                    <label>Selecione o relatório desejado</label>
                                    <asp:DropDownList ID="DropDownListRelatorios" runat="server" CssClass="textBox" 
                                        AutoPostBack="True" 
                                        Width="317px" 
                                        onselectedindexchanged="DropDownListRelatorios_SelectedIndexChanged">
                                        <asp:ListItem>Valor arrecadado no mês</asp:ListItem>
                                        <asp:ListItem>Valores a serem arrecados por mês</asp:ListItem>
                                        <asp:ListItem>Alunos inadimplentes</asp:ListItem>
                                        <asp:ListItem Selected="True">Valores devidos pelos inadimplentes por mês</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Mês de Referência</label>&nbsp;<asp:TextBox ID="txtMesReferencia" 
                                        runat="server" Width="143px"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Ano de Referência</label>&nbsp;<asp:TextBox ID="txtAnoReferencia" 
                                        runat="server" Width="143px"></asp:TextBox>
                                </li>

                                
                            </ul>
                            
                            <asp:Button ID="ButtonGerarRelatorio" runat="server" Text="Gerar Relatório" 
                                CssClass="botaoFormTick" onclick="ButtonGerarRelatorio_Click"/>
                        </fieldset>
                    </div>
                    <br />
                    <div class="formGenerico" id="inadimplentes" visible='false'>
                        <fieldset visible='false'>
                        <ul>
                            <li>
                                <center><asp:Label ID="lblTituloRelatorio" runat="server" Text="Relatório dos Alunos" Font-Size=Medium Font-Bold=true></asp:Label>
                                </center>
                            </li>
                            <li>
                            <center>
                                <asp:Label ID="lblDataAtual" runat="server" Text="01/04/2009"></asp:Label>
                            </center>
                            </li>
                            <li>
                            <asp:label ID="textLBL1" runat="server" Text="Número de inadimplentes: "> </asp:label>
                            <asp:Label ID="lbl1" runat="server" Text=" 5 "></asp:Label>
                            </li>
                            <li>
                            <ASP:label ID="textLBL2" runat="server" Text="Valor Total não recebido: "></asp:label>
                            <asp:Label ID="lbl2" runat="server" Text="R$1056,60"></asp:Label>
                            </li>
                            <li>
                            <asp:label ID="textLBL3" runat="server" Text="Média de dias atrasados: "></asp:label>
                            <asp:Label ID="lbl3" runat="server" Text="7,5"></asp:Label>
                            </li>
                            <li>
                            <asp:label ID="textLBL4" runat="server" Text="Maior Atraso: "></asp:label>
                            <asp:Label ID="lbl4" runat="server" Text="20 dias"></asp:Label>
                            </li>
                        </ul>
                            <br />
                            <center >
                            <asp:GridView ID="GridViewInadimplentes" runat="server" Width="630px" 
                                    onselectedindexchanged="GridViewInadimplentes_SelectedIndexChanged">
                            </asp:GridView>
                            </center>
                        </fieldset>
                        
                    </div>
                </div>
            </div>
    </div>

</asp:Content>
