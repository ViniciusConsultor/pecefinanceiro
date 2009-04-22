using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Vsf.Modelo;

namespace PeceFinanceiro
{
    public partial class PeceFinanceiro : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se o usuário está carregado na sessão
            if (Session["usuario"] != null)
            {
                //carrega o nome do usuário na página
                Usuario usuario = (Usuario)Session["usuario"];
                LoginName.Text = usuario.Nome != "" ? usuario.Nome : usuario.Login;

                //verifica acesso de administrador
                if (!usuario.isAdmin())
                {
                     PlHolderMenuItemRelatorios.Visible = false;
                     PlHolderMenuItemUsuarios.Visible = false;
                }
            }
            else
            {
                //Se o usuario não está na sesão, então ele não está logado. 
                ButtonLogout_Click(new object(),new EventArgs());
                LoginName.Text= "Usuário Não Identificado";  
            }
            
            
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        

       

        

        
    }
}
