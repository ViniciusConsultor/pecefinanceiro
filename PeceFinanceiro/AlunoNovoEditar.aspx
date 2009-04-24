<%@ Page Language="C#" MasterPageFile="~/PeceFinanceiro.Master" AutoEventWireup="true" CodeBehind="AlunoNovoEditar.aspx.cs" Inherits="PeceFinanceiro.CadastroAluno" Title="PECE Financeiro - Aluno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
      this.setMenuAtivo("MenuItemAlunos");
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
                            <asp:Label ID="MensagemSucesso" runat="server">Cadastro realizado com sucesso! <a href="ParcelamentoEditar.aspx">Editar Parcelas</a></asp:Label>
                        </div>
                    </asp:Panel>
                    <div class="formGenerico">
                        <fieldset>
                            <legend>Dados do Aluno</legend>
                            <ul>
                                <li>
                                    <label>Número PECE</label>
                                    <asp:TextBox ID="TextBoxNumeroPece" runat="server" Width="20%" 
                                        CssClass="textBox" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumeroPECE" 
                                        runat="server" ErrorMessage="* Campo Obrigatório" CssClass="DivErro" 
                                        ControlToValidate="TextBoxNumeroPece"></asp:RequiredFieldValidator>
                                    
                                </li>
                                <li>
                                    <label>Nome</label>
                                    <asp:TextBox ID="TextBoxNome" runat="server" CssClass="textBox" Width="434px"></asp:TextBox>
                               </li>
                               <li>
                                    <label>Endereço</label>
                                    <asp:TextBox ID="TextBoxEndereco" runat="server" CssClass="textBox" Width="434px"></asp:TextBox>
                               </li>
                               <li>
                                    <label>Telefone</label>
                                    <asp:TextBox ID="TextBoxTelefone" runat="server" CssClass="textBox" 
                                        Width="138px"></asp:TextBox>
                               </li>
                                <li>
                                    <label>Projetos Disponíveis</label>
                                    <asp:ListBox ID="ListBoxProjetosDisponiveis" runat="server" 
                                          Width="166px" AppendDataBoundItems="False" OnSelectedIndexChanged ="ButtonAdicionarProjeto_Click" DataTextField="Nome" DataValueField="Codigo"></asp:ListBox>
                                    <asp:Button ID="ButtonAdicionarProjeto" runat="server" Text="Adicionar" 
                                        CssClass="botao" onclick="ButtonAdicionarProjeto_Click" />
                                    
                                </li>
                                <li>&nbsp;</li>
                                <li>
                                    <label>Projetos Matriculados</label>
                                    <asp:ListBox ID="ListBoxProjetosMatriculados" runat="server" Rows="5" Width="40%" 
                                        CssClass="textBox" >
                                    </asp:ListBox>
                                    <asp:Button ID="ButtonRemoverProjeto" runat="server" Text="Remover" 
                                        CssClass="botao" onclick="ButtonRemoverProjeto_Click" />
                                    <asp:Label ID="LabelMensagem" runat="server" ForeColor="Red" Text="Label" 
                                        Visible="False"></asp:Label>
                                </li>
                                <li>
                                    <label>Projetos Com Registro Financeiro</label>
                                    <asp:ListBox ID="ListBoxProjetosComRegistroFinanceiro" runat="server" Rows="5" Width="40%" 
                                        CssClass="textBox"  Enabled="False">
                                        
                                    </asp:ListBox>
                                    
                                </li>
                            </ul>
                            <asp:Button ID="ButtonCadastrar" runat="server" Text="Salvar" 
                                CssClass="botaoFormTick" onclick="ButtonCadastrar_Click"/>
                            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" 
                                CssClass="botaoFormCross" onclick="ButtonCancelar_Click"/>
                            <asp:Button ID="Button1" runat="server" Text="Limpar Campos" CssClass="botaoForm"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>

</asp:Content>
