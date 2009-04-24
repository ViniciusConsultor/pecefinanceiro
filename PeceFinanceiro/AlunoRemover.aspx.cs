using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vsf.Negocio;
using Vsf.Modelo;
using Vsf.Common;

namespace PeceFinanceiro
{
    public partial class AlunoRemover : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["numeropece"] != null)
            {
                Int32 numeropece = Convert.ToInt32(Request.QueryString["numeropece"]);
                AlunoNegocio alunonegocio = new AlunoNegocio();
                Aluno aluno = alunonegocio.ObterAlunoPeloNumeroPece(numeropece);
                Logger.Registrar(((Usuario)Session["usuario"]).IdUsuario, "Excluindo aluno " + aluno.Nome + "Numero Pece : " + aluno.NumeroPece);
                if (alunonegocio.RemoverAluno(numeropece))
                {
                    Logger.Registrar(((Usuario)Session["usuario"]).IdUsuario, "Aluno " + aluno.Nome + " Excluido com sucesso!");
                    Response.Redirect("AlunoLista.aspx");
                }
                else
                {
                    LabelErro.Text = "Erro ao remover usuário";
                }
            }
            else
            {
                LabelErro.Text = "Erro de sessão";
            }

        }
    }
}
