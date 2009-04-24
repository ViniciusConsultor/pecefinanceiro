<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="Relatorios.aspx.cs" Inherits="PeceFinanceiro.Relatorios" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
      this.setMenuAtivo("MenuItemRelatorios");
      function pagePrint() {
          var divimpressao = document.getElementById('Relatorios');
          if(divimpressao.innerHTML != '')
          window.open(",").document.write(divimpressao.innerHTML + 
          '<input type=button onclick="window.print();" value=Print>');
      }
</script>

    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Cadastro Financeiro</legend>
                    <asp:Panel ID="PanelSucesso" runat="server" Visible="False">
                        <div class="DivMensagem">
                            Não foram encontrados registros especificados 
                        </div>
                    </asp:Panel>
                            <ul>
                                <li>
                                    <label>Selecione o relatório desejado</label>&nbsp;
                                    <asp:DropDownList ID="DropDownListRelatorios" runat="server" CssClass="textBox" 
                                        AutoPostBack="false" 
                                        Width="317px" 
                                        onselectedindexchanged="DropDownListRelatorios_SelectedIndexChanged">
                                        <asp:ListItem>Valor arrecadado no mês</asp:ListItem>
                                        <asp:ListItem>Valores a serem arrecados por mês</asp:ListItem>
                                        <asp:ListItem>Alunos inadimplentes</asp:ListItem>
                                        <asp:ListItem Selected="True">Valores devidos pelos inadimplentes por mês</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Mês de Referência</label>&nbsp;
                                    <asp:DropDownList ID="mnMesReferencia" runat="server" CssClass="textBox" 
                                        AutoPostBack="false" 
                                        Width="139px" 
                                        onselectedindexchanged="DropDownListRelatorios_SelectedIndexChanged" 
                                        Height="24px">
                                        <asp:ListItem Value="1">Janeiro</asp:ListItem>
                                        <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                                        <asp:ListItem Value="3">Março</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">Abril</asp:ListItem>
                                        <asp:ListItem Value="5">Maio</asp:ListItem>
                                        <asp:ListItem Value="6">Junho</asp:ListItem>
                                        <asp:ListItem Value="7">Julho</asp:ListItem>
                                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                                        <asp:ListItem Value="9">Setembro</asp:ListItem>
                                        <asp:ListItem Value="10">Outubro</asp:ListItem>
                                        <asp:ListItem Value="11">Novembro</asp:ListItem>
                                        <asp:ListItem Value="12">Dezembro</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>Ano de Referência</label>&nbsp;&nbsp;<asp:TextBox ID="txtAnoReferencia" 
                                        runat="server" Width="136px"></asp:TextBox>
                                </li>

                                
                            </ul>
                            
                            <asp:Button ID="ButtonGerarRelatorio" runat="server" Text="Gerar Relatório" 
                                CssClass="botaoFormTick" onclick="ButtonGerarRelatorio_Click"/>
                            <asp:Button ID="bttImpressao" runat="server" Text="Versão de Impressão" 
                                CssClass="botaoFormTick" Visible="False" Width="176px" />
                        </fieldset>
                    </div>
                    <br />
                    <div class="formGenerico" id="Relatorios" >
                        <fieldset >
                        <div>
                        <div style="height:15px"></div>
                            <div style="height:40px;">
                                <center><asp:Label ID="lblTituloRelatorio" runat="server" Text="Relatório dos Alunos" Font-Size=Medium Font-Bold=true></asp:Label>
                                    <br />
                                </center>
                            </div>
                            <div style="height:30px;">
                            <center>
                                <asp:Label ID="lblDataAtual" runat="server" Text=""></asp:Label>
                                <br />
                            </center>
                            </div>
                            <div style="position:relative;left:27px; top: 0px; width: 625px;">
                            <div>
                            <asp:label ID="textLBL1" runat="server" Text="Número de inadimplentes: "> </asp:label>
                            <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                            <ASP:label ID="textLBL2" runat="server" Text="Valor Total não recebido: "></asp:label>
                            <asp:Label ID="lbl2" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                            <asp:label ID="textLBL3" runat="server" Text="Média de dias atrasados: "></asp:label>
                            <asp:Label ID="lbl3" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                            <asp:label ID="textLBL4" runat="server" Text="Maior Atraso: "></asp:label>
                            <asp:Label ID="lbl4" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="height:10px"></div>
                            </div>
                            
                        </div>
                            <br />
                            <center >
                            <asp:GridView ID="GridViewInadimplentes" runat="server" Width="630px" 
                                    onselectedindexchanged="GridViewInadimplentes_SelectedIndexChanged" 
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <RowStyle BackColor="#F7F7DE" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            
                            </center>
                        </fieldset>

                    </div>
                </div>
            </div>
    </div>

</asp:Content>
