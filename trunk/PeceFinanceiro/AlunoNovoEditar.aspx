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
                                        CssClass="textBox" CausesValidation="True" ></asp:TextBox>
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
                                         DataTextField="Nome" DataValueField="Codigo" 
                                        Width="166px" DataSourceID="ListaProjetosDisponiveisDoAluno" 
                                        onselectedindexchanged="ButtonAdicionarProjeto_Click"></asp:ListBox>
                                    <asp:Button ID="ButtonAdicionarProjeto" runat="server" Text="Adicionar" 
                                        CssClass="botao" onclick="ButtonAdicionarProjeto_Click" />
                                    <asp:ObjectDataSource ID="ListaProjetosDisponiveis" runat="server" 
                                        SelectMethod="ObterTodosProjetos" TypeName="Vsf.Negocio.ProjetoNegocio">
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="ListaProjetosDisponiveisDoAluno" runat="server" 
                                        SelectMethod="ObterProjetosDisponiveisAoAluno" 
                                        TypeName="Vsf.Negocio.ProjetoNegocio">
                                        <SelectParameters>
                                            <asp:QueryStringParameter DefaultValue="0" Name="codigoPece" 
                                                QueryStringField="IdAluno" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </li>
                                <li>
                                    &nbsp;</li>
                                <li>
                                    <label>Projetos Matriculados</label>
                                    <asp:ListBox ID="ListBoxProjetosMatriculados" runat="server" Rows="5" Width="40%" 
                                        CssClass="textBox" DataSourceID="ListaTodosProjetosdoAluno" 
                                        DataTextField="Nome" DataValueField="Codigo">
                                    </asp:ListBox>
                                    <asp:ObjectDataSource ID="ListaTodosProjetosdoAluno" runat="server" 
                                        SelectMethod="ObterProjetosDoAluno" TypeName="Vsf.Negocio.ProjetoNegocio">
                                        <SelectParameters>
                                            <asp:QueryStringParameter DefaultValue="0" Name="codigoPece" 
                                                QueryStringField="idAluno" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:Button ID="ButtonRemoverProjeto" runat="server" Text="Remover" 
                                        CssClass="botao" onclick="ButtonRemoverProjeto_Click" />
                                    <asp:Label ID="LabelMensagem" runat="server" ForeColor="Red" Text="Label" 
                                        Visible="False"></asp:Label>
                                </li>
                            </ul>
                            <asp:Button ID="ButtonCadastrar" runat="server" Text="Salvar" 
                                CssClass="botaoFormTick" onclick="ButtonCadastrar_Click"/>
                            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" CssClass="botaoFormCross"/>
                            <asp:Button ID="Button1" runat="server" Text="Limpar Campos" CssClass="botaoForm"/>
                        </fieldset>
                    </div>
                </div>
            </div>
    </div>

</asp:Content>
