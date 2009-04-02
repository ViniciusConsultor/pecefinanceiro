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

namespace PeceFinanceiro
{
    public partial class CadastroFinanceiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TextBoxValorComAjuste.Enabled = false;
            this.TextBoxAjusteValorFinal.Attributes.Add("onkeydown", "ForceNumericInput(event, this, true, false);");
            this.TextBoxAjusteValorFinal.Attributes.Add("onkeyup", "AtualizaValores();");
            this.TextBoxDiaPagamento.Attributes.Add("onkeydown", "ForceNumericInput(event, this, false, false)");
            this.TextBoxNumeroParcelas.Attributes.Add("onkeydown", "ForceNumericInput(event, this, false, false)");
            this.TextBoxNumeroParcelas.Attributes.Add("onkeyup", "AtualizaValores();");
        }
    }
}
