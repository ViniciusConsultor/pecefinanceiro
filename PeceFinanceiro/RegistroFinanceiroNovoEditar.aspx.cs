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
        RegistroFinanceiro registroFinanceiroGlobal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindJSEvents();

            if (!IsPostBack)
            {
                FillComboProjetos();
            }

            int idRegistroSelecionado = -1;
            if (!(Request.QueryString["idMatricula"] == null))
            {
                HiddenFieldEditando.Value = "true";
                idRegistroSelecionado = Int32.Parse(Request.QueryString["idMatricula"]);
                RegistroFinanceiroNegocio registroNegocio = new RegistroFinanceiroNegocio();
                registroFinanceiroGlobal = registroNegocio.ObterRegistroPorMatricula(idRegistroSelecionado);

                SelecionaProjeto(registroFinanceiroGlobal.AlunoProjeto.Projeto.Codigo);
                SelecionaDdlAluno(registroFinanceiroGlobal.AlunoProjeto.Aluno.NumeroPece);
                if(!IsPostBack) FillAllFieldsToEdit(registroFinanceiroGlobal);
            }
            else
            {
                if (!IsPostBack) SelecionaProjeto("");
            }

            VerificaAlunoSelecionado();

            SetEnabledFieldsWhileEditing(Boolean.Parse(HiddenFieldEditando.Value));

            PanelSucesso.Visible = false;
            PanelErro.Visible = false;
        }

        private void VerificaAlunoSelecionado()
        {
            if (DropDownListAlunos.Items.Count == 0)
            {
                ShowErrorMessage("Não é possível incluir registro pois não há alunos ou alunos sem registro para o projeto selecionado.");
                MudaStatusForm(false);
            }
            else
                MudaStatusForm(true);
        }

        private void MudaStatusForm(bool enabled)
        {
            this.TextBoxAjusteValorFinal.Enabled = enabled;
            this.TextBoxDataPrimeiraParcela.Enabled = enabled;
            this.TextBoxDiaPagamento.Enabled = enabled;
            this.TextBoxNumeroParcelas.Enabled = enabled;
            this.TextBoxObservacoes.Enabled = enabled;
            this.TextBoxValorComAjuste.Enabled = enabled;
            this.TextBoxValorParcela.Enabled = enabled;
            this.ButtonCadastrar.Enabled = enabled;

        }

        private void FillAllFieldsToEdit(RegistroFinanceiro registroFinanceiro)
        {
            this.TextBoxValorComAjuste.Text = registroFinanceiro.PrecoReajustado.ToString("#0.00").Replace('.', ',');
            this.HiddenValorComAjuste.Value = registroFinanceiro.PrecoReajustado.ToString("#0.00").Replace('.', ',');
            this.TextBoxNumeroParcelas.Text = Convert.ToString(registroFinanceiro.NumeroParcelas);
            this.TextBoxDiaPagamento.Text = Convert.ToString(registroFinanceiro.DiaPagamento);
            this.TextBoxDataPrimeiraParcela.Text = Convert.ToString(registroFinanceiro.DataVencimentoPrimeiraParcela.ToString("d", _culture));
            this.TextBoxObservacoes.Text = registroFinanceiro.Observacoes;

            SetEnabledFieldsWhileEditing(true);
        }

        private void SetEnabledFieldsWhileEditing(bool editing)
        {
            this.TextBoxValorComAjuste.ReadOnly = !editing;
            this.PlaceHolderValorComAjuste.Visible = !editing;
            this.PlaceHolderValorParcela.Visible = !editing;
            this.ButtonEditarParcelas.Visible = editing;
            this.DropDownListAlunos.Enabled = !editing;
            this.DropDownListProjetos.Enabled = !editing;
            this.TextBoxValorComAjuste.ReadOnly = !editing;
            this.TextBoxValorParcela.ReadOnly = !editing;
            this.TextBoxDataPrimeiraParcela.ReadOnly = !editing;
        }

        private void SelecionaDdlAluno(int idAluno)
        {
            int i = 0;
            foreach (ListItem item in this.DropDownListAlunos.Items)
            {
                if (Int32.Parse(item.Value) == idAluno)
                {
                    this.DropDownListAlunos.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        private void BindJSEvents()
        {
            this.TextBoxAjusteValorFinal.Attributes.Add("onkeydown", "ForceNumericInput(event, this, true, false);");
            this.TextBoxAjusteValorFinal.Attributes.Add("onkeyup", "AtualizaValores();");
            this.TextBoxDiaPagamento.Attributes.Add("onkeydown", "ForceNumericInput(event, this, false, false)");
            this.TextBoxNumeroParcelas.Attributes.Add("onkeydown", "ForceNumericInput(event, this, false, false)");
            this.TextBoxValorComAjuste.Attributes.Add("onkeydown", "ForceNumericInput(event, this, true, false)");
            this.TextBoxValorComAjuste.Attributes.Add("onkeyup", "AtualizaValores();");
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
            
            List<Aluno> alunos = new List<Aluno>();

            AlunoNegocio alunoNegocio = new AlunoNegocio();
            alunos = alunoNegocio.ObterAlunosPorProjetoSemRegistroFinanceiro(projeto.Codigo);

            this.DropDownListAlunos.DataSource = alunos;

            this.DropDownListAlunos.DataTextField = "Nome";
            this.DropDownListAlunos.DataValueField = "NumeroPece";
            this.DropDownListAlunos.DataBind();
        }

        protected void DropDownListProjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelecionaProjeto(String.Empty);
        }

        private void SelecionaProjeto(string idProjeto)
        {
            ProjetoNegocio projetoNegocio = new ProjetoNegocio();
            Projeto projeto;
            if (!idProjeto.Equals(String.Empty))
            {
                projeto = projetoNegocio.ObterProjetoPorCodigo(idProjeto);
                ChangeDdlProjetosSelectedItem(idProjeto);
            }
            else
                projeto = projetoNegocio.ObterProjetoPorCodigo(this.DropDownListProjetos.SelectedValue);


            this.HiddenFieldValorCurso.Value = Convert.ToString(projeto.Valor).Replace('.', ',');
            this.LabelValorFinal.Text = "R$ " + projeto.Valor.ToString("#0.00").Replace('.', ',');

            FillComboAlunos(projeto);
            VerificaAlunoSelecionado();
        }

        private void ChangeDdlProjetosSelectedItem(string idProjeto)
        {
            int i = 0;
            foreach (ListItem item in this.DropDownListProjetos.Items)
            {
                if (item.Value.Equals(idProjeto))
                {
                    this.DropDownListProjetos.SelectedIndex = i;
                    break;
                }
                i++;
            }
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
            string errorMessage = "Ocorreram erros durante o processamento. <ul>";

            RegistroFinanceiro registroFinanceiro = new RegistroFinanceiro();
            DateTime dtVencimentoParcela = new DateTime();
            if (DateTime.TryParse(this.TextBoxDataPrimeiraParcela.Text, _culture, DateTimeStyles.None, out dtVencimentoParcela))
                registroFinanceiro.DataVencimentoPrimeiraParcela = dtVencimentoParcela;
            else
            {
                errorOccured = true;
                errorMessage += "<li>Data de Vencimento da Primeira parcela inválida</li>";
            }
            
            int diaPagamento = 0;
            if (Int32.TryParse(this.TextBoxDiaPagamento.Text, out diaPagamento))
                registroFinanceiro.DiaPagamento = diaPagamento;
            else
            {
                errorOccured = true;
                errorMessage += "<li>Dia de vencimento de parcelas deve ser preenchido</li>";
            }

            int intNumeroParcelas = 0;
            if (Int32.TryParse(this.TextBoxNumeroParcelas.Text, out intNumeroParcelas))
                registroFinanceiro.NumeroParcelas = intNumeroParcelas;
            else
            {
                errorOccured = true;
                errorMessage += "<li>Número de parcelas deve ser preenchido</li>";
            }

            errorMessage += "</ul>";
            
            registroFinanceiro.Observacoes = this.TextBoxObservacoes.Text;
            
            Double valorReajustado = 0.0;
            if (Double.TryParse(this.HiddenValorComAjuste.Value, NumberStyles.Currency, _culture, out valorReajustado) && valorReajustado > 0)
                registroFinanceiro.PrecoReajustado = valorReajustado;
            else
            {
                errorOccured = true;
                errorMessage += "Valor inválido para o valor final do curso.";
            }
            registroFinanceiro.Status = StatusAlunoProjeto.EmDia;

            AlunoNegocio alunoNegocio = new AlunoNegocio();
            AlunoProjeto alunoProjeto = alunoNegocio.ObterRelacionamentoAlunoProjeto(Int32.Parse(DropDownListAlunos.SelectedValue), DropDownListProjetos.SelectedValue);

            registroFinanceiro.AlunoProjeto = alunoProjeto;

            RegistroFinanceiroNegocio financeiroNegocio = new RegistroFinanceiroNegocio();
            if (errorOccured)
            {
                ShowErrorMessage(errorMessage);
            }
            else
            {

                if (!Boolean.Parse(HiddenFieldEditando.Value))
                {
                    if (financeiroNegocio.IncluirRegistroFinanceiro(registroFinanceiro, alunoProjeto))
                    {
                        ShowSuccessMessage("Cadastro realizado com sucesso. <a href=\"ParcelamentoEditar.aspx?idRegistro=" + registroFinanceiro.AlunoProjeto.Id + "\">Clique aqui para editar o parcelamento deste registro.</a>");
                        ButtonEditarParcelas.Visible = true;
                    }
                }
                else
                {
                    if(financeiroNegocio.AtualizarRegistroFinanceiro(registroFinanceiro))
                        ShowSuccessMessage("Cadastro atualizado com sucesso."); ;
                }
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
            Response.Redirect("RegistroFinanceiroLista.aspx");
        }

        protected void ButtonEditarParcelas_Click(object sender, EventArgs e)
        {
            if(registroFinanceiroGlobal != null)
                Response.Redirect("ParcelamentoEditar.aspx?idRegistro=" + registroFinanceiroGlobal.IdRegistro);
        }


    }
}
