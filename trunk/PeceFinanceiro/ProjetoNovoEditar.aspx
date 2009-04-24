<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="ProjetoNovoEditar.aspx.cs" Inherits="PeceFinanceiro.CadastroProjeto" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemProjetos");
</script>
    <div id="container">
            <div id="primarycontainer">
                <div id="primarycontent">
                <asp:Panel ID="PanelErro" runat="server">
                     <div class="DivErro">
                            <asp:Label ID="MensagemErro" runat="server"></asp:Label><br />
                            
                       </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelSucesso" runat="server">
                        <div class="DivSucesso">
                            <asp:Label ID="MensagemSucesso" runat="server">Cadastro realizado com sucesso! <a href="ProjetoNovoEditar.aspx">Editar Projeto</a></asp:Label>
                        </div>
                    </asp:Panel>
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Dados do Projeto</legend>
                            <asp:HiddenField ID="HiddenFieldEditando" runat="server" Value="false" />
                            <ul>
                                <li>
                                    <label>Código do Projeto</label>
                                    <asp:TextBox ID="TextBoxCodigoProjeto" runat="server" CssClass="textBox" Width="300px"></asp:TextBox>
                                </li>
                                <li>
                                    <label>Nome do Projeto</label>
                                    <asp:TextBox ID="TextBoxNomeProjeto" runat="server" CssClass="textBox" Width="300px"></asp:TextBox>
                               </li>
                               <li>
                                    <label>Descrição do Projeto</label>
                                    <asp:TextBox ID="TextBoxDescricaoProjeto" runat="server" CssClass="textBox" Width="300px"></asp:TextBox>
                               </li>
                               <li>
                                    <label>Valor do Projeto</label>
                                    <asp:TextBox ID="TextValorProjeto" runat="server" CssClass="textBox" Width="300px"></asp:TextBox>
                               </li>
                                <li>
                                &nbsp;</li>
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

</asp:Content>
