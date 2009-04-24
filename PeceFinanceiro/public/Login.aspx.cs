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
using Vsf.Negocio;

namespace PeceFinanceiro
{
    public partial class Login : System.Web.UI.Page
    {
        Usuario _usuario = new Usuario();
        UsuarioNegocio _usuarionegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginFinanceiro.Focus();
        }

        protected void LoginFinanceiro_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (_usuarionegocio.Autenticar(LoginFinanceiro.UserName, LoginFinanceiro.Password))
            {
                _usuario = _usuarionegocio.ConsultarUsuario(LoginFinanceiro.UserName);
               Session.Add("usuario",_usuario);
               Session.Add("login", _usuario.Login);
               Session.Add("isadmin", _usuario.isAdmin() ? true : false);
               FormsAuthentication.RedirectFromLoginPage(LoginFinanceiro.UserName, true);
            }
        }
    }
}
