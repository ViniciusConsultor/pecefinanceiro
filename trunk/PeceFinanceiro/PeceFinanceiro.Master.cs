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

namespace PeceFinanceiro
{
    public partial class PeceFinanceiro : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                      
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

       

        

        
    }
}
