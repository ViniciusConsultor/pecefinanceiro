using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vsf.Modelo;
using Vsf.Negocio;

namespace PeceFinanceiro
{
    public partial class UsuarioRemover : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["login"] != null)
            {
                String login = Request.QueryString["login"].ToString();
                UsuarioNegocio usuarionegocio = new UsuarioNegocio();
                if (usuarionegocio.RemoverUsuario(login))
                {
                    Response.Redirect("UsuarioLista.aspx");
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


