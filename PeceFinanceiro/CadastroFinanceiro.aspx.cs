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
using System.Collections.Generic;
using Vsf.Negocio;

namespace PeceFinanceiro
{
    public partial class CadastroFinanceiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindJSEvents();

            if (!IsPostBack)
            {
                FillComboProjetos();
            }
        }

        private void BindJSEvents()
        {
            this.TextBoxValorComAjuste.Enabled = false;
            this.TextBoxAjusteValorFinal.Attributes.Add("onkeydown", "ForceNumericInput(event, this, true, false);");
            this.TextBoxAjusteValorFinal.Attributes.Add("onkeyup", "AtualizaValores();");
            this.TextBoxDiaPagamento.Attributes.Add("onkeydown", "ForceNumericInput(event, this, false, false)");
            this.TextBoxNumeroParcelas.Attributes.Add("onkeydown", "ForceNumericInput(event, this, false, false)");
            this.TextBoxNumeroParcelas.Attributes.Add("onkeyup", "AtualizaValores();");
        }

        private void FillComboProjetos()
        {
            ProjetoNegocio projetoNegocio = new ProjetoNegocio();
            List<Projeto> listProjeto = projetoNegocio.ObterTodosProjetos();

            this.DropDownListProjetos.DataSource = listProjeto;
            this.DropDownListProjetos.DataTextField = "Nome";
            this.DropDownListProjetos.DataValueField = "Codigo";
            this.DropDownListProjetos.DataBind();
        }

        private void FillComboAlunos(Projeto projeto)
        {

        }
    }
}
