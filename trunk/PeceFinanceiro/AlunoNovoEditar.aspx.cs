using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vsf.Modelo;
using Vsf.Negocio;
using System.Collections.Generic;

namespace PeceFinanceiro
{
    public partial class CadastroAluno : System.Web.UI.Page
    {
        Aluno _aluno = new Aluno();
        AlunoNegocio alunoNegocio = new AlunoNegocio();
        List<Projeto> _projetosDoAluno = new List<Projeto>();
        ProjetoNegocio projetonegocio = new ProjetoNegocio();
        String _operacao;

        protected void Page_Load(object sender, EventArgs e)
        {
            _operacao = (Request.QueryString["numeropece"] != null ? "editar" : "novo");
            if (!Page.IsPostBack){
                PanelErro.Visible = false;
                PanelSucesso.Visible = false;
                
                if (_operacao.Equals("editar"))
                {
                    _aluno = alunoNegocio.ObterAlunoPeloNumeroPece(Convert.ToInt32(Request.QueryString["numeropece"]));
                    CarregarDadosDoAluno();
                    CarregarListBoxProjetosDisponiveis();
                    CarregarListBoxProjetosMatriculados();
                    BloquearMatriculasComRegistroFinanceiro();

                }
                else
                {
                    CarregarListBoxProjetosDisponiveisComTodosProjetos();
                }
            }

        }

        private void BloquearMatriculasComRegistroFinanceiro()
        {
            RegistroFinanceiroNegocio registroFinanceiroNegocio = new RegistroFinanceiroNegocio();
            AlunoProjeto matricula;
            foreach (ListItem item in ListBoxProjetosMatriculados.Items)
            {
                matricula = alunoNegocio.ObterRelacionamentoAlunoProjeto(_aluno.NumeroPece, item.Value);
                if (registroFinanceiroNegocio.ExisteRegistroFinanceiroParaMatricula(matricula))
                {
                    ListBoxProjetosComRegistroFinanceiro.Items.Add(new ListItem(item.Text,item.Value));
                    item.Enabled = false;
                }
            }
        }

        private void CarregarListBoxProjetosMatriculados()
        {
            List<Projeto> ListaProjetosMatriculados =  projetonegocio.ObterProjetosDoAluno(_aluno.NumeroPece);
            ListBoxProjetosMatriculados.DataSource = ListaProjetosMatriculados;
            ListBoxProjetosMatriculados.DataTextField = "Nome";
            ListBoxProjetosMatriculados.DataValueField = "Codigo";
            ListBoxProjetosMatriculados.DataBind();
        }

        private void CarregarListBoxProjetosDisponiveis()
        {
            List<Projeto> ListaProjetosDisponiveis = projetonegocio.ObterProjetosDisponiveisAoAluno(_aluno.NumeroPece);
            ListBoxProjetosDisponiveis.DataSource = ListaProjetosDisponiveis;
            ListBoxProjetosDisponiveis.DataTextField = "Nome";
            ListBoxProjetosDisponiveis.DataValueField = "Codigo";
            ListBoxProjetosDisponiveis.DataBind();
        }

        private void CarregarListBoxProjetosDisponiveisComTodosProjetos()
        {
            List<Projeto> ListaProjetosDisponiveis = projetonegocio.ObterTodosProjetos();
            ListBoxProjetosDisponiveis.DataSource = ListaProjetosDisponiveis;
            ListBoxProjetosDisponiveis.DataTextField = "Nome";
            ListBoxProjetosDisponiveis.DataValueField = "Codigo";
            ListBoxProjetosDisponiveis.DataBind();
        }

        private void CarregarDadosDoAluno()
        {
            int numeroPece = (Request.QueryString["numeropece"] != null ? Convert.ToInt32(Request.QueryString["numeropece"].ToString()) : 0);
            if (numeroPece != 0){
                _aluno = alunoNegocio.ObterAlunoPeloNumeroPece(numeroPece);
                TextBoxNumeroPece.Text = Convert.ToString(_aluno.NumeroPece);
                TextBoxNome.Text = _aluno.Nome;
                TextBoxTelefone.Text = _aluno.Telefone;
                TextBoxEndereco.Text = _aluno.Endereco;
                TextBoxNumeroPece.Enabled = false;
            }
            else{
                ShowErrorMessage("Erro ao carregar dados do aluno");
            }
            
        }


        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (_operacao.Equals("novo"))
                {
                    _aluno.NumeroPece = Int32.Parse(TextBoxNumeroPece.Text);
                    _aluno.Nome = TextBoxNome.Text;
                    _aluno.Endereco = TextBoxEndereco.Text;
                    _aluno.Telefone = TextBoxTelefone.Text;
                    MontaListaProjetos();
                    if (alunoNegocio.InserirAluno(_aluno))
                    {
                        if (_projetosDoAluno.Count > 0)
                        {
                            if (alunoNegocio.InserirMatriculas(_aluno, _projetosDoAluno))
                            {
                                Response.Redirect("AlunoLista.aspx?Action=AlunoInserido");
                            }
                            else
                            {
                                //Aluno Inserido mas erro na criação da matrícula
                                LabelMensagem.Text = "Aluno Inserido mas erro na criação da matrícula";
                                LabelMensagem.Visible = true;
                            }
                        }
                        else
                        {
                                Response.Redirect("AlunoLista.aspx?Action=AlunoInserido");
                        }

                    }
                    else
                    {
                        //Exibe mensagem de erro na inserção de aluno
                        LabelMensagem.Text = "erro na inserção do aluno";
                        LabelMensagem.Visible = true;
                    }

                }
                else
                {//editar
                    _aluno.Nome = TextBoxNome.Text;
                    _aluno.Endereco = TextBoxEndereco.Text;
                    _aluno.Telefone = TextBoxTelefone.Text;
                    _aluno.NumeroPece = Convert.ToInt32(TextBoxNumeroPece.Text);
                    MontaListaProjetos();
                    if (alunoNegocio.AtualizarAluno(_aluno,_projetosDoAluno))
                    {
                            Response.Redirect("AlunoLista.aspx?Action=AlunoAtualizado");
                    }
                    else
                    {
                            ShowErrorMessage("Falha ao atualizar o Aluno");
                    }
                    

                }
            }
        }

        private void MontaListaProjetos()
        {
            foreach (ListItem item in ListBoxProjetosMatriculados.Items)
            {
                _projetosDoAluno.Add(projetonegocio.ObterProjetoPorCodigo(item.Value));
            }
        }

        protected void ButtonAdicionarProjeto_Click(object sender, EventArgs e)
        {
            if (ListBoxProjetosDisponiveis.SelectedItem != null)
            {
                ListItem selecteditem = ListBoxProjetosDisponiveis.SelectedItem;
                ListBoxProjetosMatriculados.Items.Add(ListBoxProjetosDisponiveis.SelectedItem);
                ListBoxProjetosMatriculados.DataBind();
                ListBoxProjetosDisponiveis.SelectedItem.Selected = false;
                ListBoxProjetosDisponiveis.Items.Remove(selecteditem);
                
            }

        }

        protected void ButtonRemoverProjeto_Click(object sender, EventArgs e)
        {
            if (ListBoxProjetosMatriculados.SelectedItem != null)
            {
                ListBoxProjetosDisponiveis.Items.Add(ListBoxProjetosMatriculados.SelectedItem);
                ListItem selecteditem = ListBoxProjetosMatriculados.SelectedItem;
                ListBoxProjetosDisponiveis.SelectedItem.Selected = false;
                ListBoxProjetosMatriculados.Items.Remove(selecteditem);
            }
        }

        private void ShowErrorMessage(string errorMessage)
        {
            PanelSucesso.Visible = false;
            PanelErro.Visible = true;
            MensagemErro.Text = errorMessage;
        }
        private void ShowSuccessMessage(string successMessage)
        {
            PanelErro.Visible = false;
            PanelSucesso.Visible = true;
            MensagemSucesso.Text = successMessage;
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AlunoLista.aspx");
        }

        
    }
}
