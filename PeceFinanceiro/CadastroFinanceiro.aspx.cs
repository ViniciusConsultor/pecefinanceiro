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
using System.Globalization;

namespace PeceFinanceiro
{
    public partial class CadastroFinanceiro : System.Web.UI.Page
    {
        CultureInfo _culture = new CultureInfo("pt-BR"); 
        protected void Page_Load(object sender, EventArgs e)
        {
            BindJSEvents();

            if (!IsPostBack)
            {
                FillComboProjetos();
                SelecionaProjeto();
            }

            this.TextBoxValorComAjuste.ReadOnly = true;
            this.TextBoxValorParcela.ReadOnly = true;
            this.TextBoxDataPrimeiraParcela.ReadOnly = true;
            PanelSucesso.Visible = false;
            PanelErro.Visible = false;
            ButtonEditarParcelas.Visible = false;
        }

        private void BindJSEvents()
        {
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
            this.DropDownListAlunos.DataSource = projeto.Alunos;
            this.DropDownListAlunos.DataTextField = "Nome";
            this.DropDownListAlunos.DataValueField = "NumeroPece";
            this.DropDownListAlunos.DataBind();
        }

        protected void DropDownListProjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelecionaProjeto();
        }

        private void SelecionaProjeto()
        {
            ProjetoNegocio projetoNegocio = new ProjetoNegocio();
            Projeto projeto = projetoNegocio.ObterProjetoPorCodigo(this.DropDownListProjetos.SelectedValue);

            this.HiddenFieldValorCurso.Value = Convert.ToString(projeto.Valor).Replace('.', ',');
            this.LabelValorFinal.Text = "R$ " + projeto.Valor.ToString("#0.00").Replace('.', ',');

            FillComboAlunos(projeto);
        }

        protected void DropDownListAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void CalendarDataPrimeiraParcela_SelectionChanged(object sender, EventArgs e)
        {
            this.TextBoxDataPrimeiraParcela.Text = CalendarDataPrimeiraParcela.SelectedDate.ToString("d", _culture);
            CalendarVisibleSignalFromServer.Value = "false";
        }

        protected void CalendarDataPrimeiraParcela_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            CalendarVisibleSignalFromServer.Value = "true";
        }

        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            bool errorOccured = false;
            string errorMessage = String.Empty;

            RegistroFinanceiro registroFinanceiro = new RegistroFinanceiro();
            registroFinanceiro.DataVencimentoPrimeiraParcela = DateTime.Parse(this.TextBoxDataPrimeiraParcela.Text, _culture);
            registroFinanceiro.DiaPagamento = Int32.Parse(this.TextBoxDiaPagamento.Text);
            registroFinanceiro.NumeroParcelas = Int32.Parse(this.TextBoxNumeroParcelas.Text);
            registroFinanceiro.Observacoes = this.TextBoxObservacoes.Text;
            Double valorReajustado = 0.0;
            if (Double.TryParse(this.HiddenValorComAjuste.Value.Replace(',','.'), out valorReajustado) && valorReajustado > 0)
                registroFinanceiro.PrecoReajustado = valorReajustado;
            else
            {
                errorOccured = true;
                errorMessage = "Valor inválido para o valor final do curso.";
            }
            registroFinanceiro.Status = StatusRegistroFinanceiro.EmDia;

            AlunoNegocio alunoNegocio = new AlunoNegocio();
            AlunoProjeto alunoProjeto = alunoNegocio.ObterRelacionamentoAlunoProjeto(Int32.Parse(DropDownListAlunos.SelectedValue), DropDownListProjetos.SelectedValue);

            RegistroFinanceiroNegocio financeiroNegocio = new RegistroFinanceiroNegocio();
            if (financeiroNegocio.IncluirRegistroFinanceiro(registroFinanceiro, alunoProjeto))
            {
                PanelSucesso.Visible = true;
                ButtonEditarParcelas.Visible = true;
            }
            else
            {
                PanelErro.Visible = true;
                MensagemErro.Text = errorMessage;
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaCadastros.aspx");
        }


    }
}
