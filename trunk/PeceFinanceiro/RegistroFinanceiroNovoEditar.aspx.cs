﻿using System;
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
        RegistroFinanceiro registroFinanceiro = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindJSEvents();

            if (!IsPostBack)
            {
                FillComboProjetos();

                int idRegistroSelecionado = -1;
                if (!(Request.QueryString["idRegistro"] == null))
                {
                    idRegistroSelecionado = Int32.Parse(Request.QueryString["idRegistro"]);
                    RegistroFinanceiroNegocio registroNegocio = new RegistroFinanceiroNegocio();
                    registroFinanceiro = registroNegocio.ObterRegistroPorId(idRegistroSelecionado);

                    SelecionaProjeto(registroFinanceiro.AlunoProjeto.Projeto.Codigo);
                    SelecionaDdlAluno(registroFinanceiro.AlunoProjeto.Aluno.NumeroPece);
                    FillAllFieldsToEdit(registroFinanceiro);
                }
                else
                {
                    SelecionaProjeto("");
                }

            }

            VerificaAlunoSelecionado();

            this.TextBoxValorComAjuste.ReadOnly = true;
            this.TextBoxValorParcela.ReadOnly = true;
            this.TextBoxDataPrimeiraParcela.ReadOnly = true;
            PanelSucesso.Visible = false;
            PanelErro.Visible = false;
            ButtonEditarParcelas.Visible = false;
        }

        private void VerificaAlunoSelecionado()
        {
            if (DropDownListAlunos.Items.Count == 0)
            {
                ShowErrorMessage("Não é possível incluir registro pois não há aluno para o projeto selecionado.");
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
            this.TextBoxNumeroParcelas.Text = Convert.ToString(registroFinanceiro.NumeroParcelas);
            this.TextBoxDiaPagamento.Text = Convert.ToString(registroFinanceiro.DiaPagamento);
            this.TextBoxDataPrimeiraParcela.Text = Convert.ToString(registroFinanceiro.DataVencimentoPrimeiraParcela);

            this.TextBoxValorComAjuste.ReadOnly = false;
            this.PlaceHolderValorComAjuste.Visible = false;
            this.PlaceHolderValorParcela.Visible = false;
            this.ButtonEditarParcelas.Visible = true;
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
            registroFinanceiro.Status = StatusAlunoProjeto.EmDia;

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
                ShowErrorMessage(errorMessage);
            }
        }

        private void ShowErrorMessage(string errorMessage)
        {
            PanelErro.Visible = true;
            MensagemErro.Text = errorMessage;
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroFinanceiroLista.aspx");
        }


    }
}
