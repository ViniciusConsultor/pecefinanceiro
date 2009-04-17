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
            LabelMensagem.Visible = false;
            String operacao = (Request.QueryString["operacao"] !=null ? Request.QueryString["operacao"].ToString() : "");
            if (operacao.Equals("editar"))
            {
                _operacao = "editar";
            }
            else
            {
                _operacao = "novo";
            }

        }
        protected List<Projeto> ObterProjetosDoAluno(int codigoPece)
        {
            return projetonegocio.ObterProjetosDoAluno(codigoPece);
        }

       
       

        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            if (_operacao.Equals("novo"))
            {
                _aluno.NumeroPece=Int32.Parse(TextBoxNumeroPece.Text);
                _aluno.Nome=TextBoxNome.Text;
                _aluno.Telefone = String.Empty;
                _aluno.Endereco = String.Empty;
                MontaListaProjetos();
                if (alunoNegocio.InserirAluno(_aluno))
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
                    //Exibe mensagem de erro na inserção de aluno
                    LabelMensagem.Text = "erro na inserção do aluno";
                    LabelMensagem.Visible = true;
                }

            }
            else
            {
                
                
                
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
                ListBoxProjetosMatriculados.Items.Add(selecteditem);
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
    }
}
