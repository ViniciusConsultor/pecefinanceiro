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
using System.Globalization;
using Vsf.Modelo;
using Vsf.Negocio;
using System.Collections.Generic;

namespace PeceFinanceiro
{
    public partial class RegistrarPagamentos : System.Web.UI.Page
    {
        CultureInfo _culture = new CultureInfo("pt-BR");
        Int32 _idRegistro = -1;
        Int32 _idMatricula = -1;
        RegistroFinanceiro registro;
        protected void Page_Load(object sender, EventArgs e)
        {
            _idMatricula = Convert.ToInt32(Request.QueryString["idMatricula"]);
            this.Culture = "pt-BR";

            RegistroFinanceiroNegocio registroNegocio = new RegistroFinanceiroNegocio();
            registro = registroNegocio.ObterRegistroPorMatricula(_idMatricula);
            _idRegistro = registro.IdRegistro;

            if (!IsPostBack)
            {
                FillGrids();
                FillSummary();
                this.Button2.Attributes.Add("onmousedown", "javascript:WriteStrings();");
            }
        }

        private void FillGrids()
        {
            ParcelaNegocio parcelaNegocio = new ParcelaNegocio();
            List<Parcela> listParcelas = parcelaNegocio.ObterParcelasPorRegistro(_idRegistro);
            this.GridViewParcelas.DataSource = listParcelas;

            GridViewParcelas.AutoGenerateColumns = false;
            GridViewParcelas.DataBind();
        }

        private void FillSummary()
        {
            this.LabelNomeAluno.Text = registro.AlunoProjeto.NomeAluno;
            this.LabelNumeroParcelas.Text = Convert.ToString(registro.NumeroParcelas);
            this.LabelValor.Text = registro.PrecoReajustado.ToString("R$ #0.00");
            this.HiddenFieldValorTotal.Value = registro.PrecoReajustado.ToString("#0.00");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroFinanceiroNovoEditar.aspx?idMatricula=" + _idMatricula);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            bool sucesso = true;

            //Dictionary<DateTime, Double> dictionaryValores = new Dictionary<DateTime, double>();
            string[] strHidden = this.HiddenFieldDados.Value.Split('&');
            string[] datasString = strHidden[0].Split(';');
            string[] valoresString = strHidden[1].Split(';');

            ParcelaNegocio parcelaNegocio = new ParcelaNegocio();

            int i = 0;
            foreach (string strPagamento in datasString)
            {
                string strValor = valoresString[i];
                if (strPagamento != String.Empty && strValor != String.Empty)
                {
                    DateTime dtPagamento;
                    Double valor;
                    if (Double.TryParse(strValor, out valor) &&
                        DateTime.TryParse(strPagamento, out dtPagamento) &&
                        valor >= 0 &&
                        dtPagamento <= DateTime.Now)
                    {
                        if (!parcelaNegocio.RegistrarPagamento(new Parcela(i + 1, dtPagamento, valor, true), _idRegistro))
                        {
                            sucesso = false;
                            throw new Exception("Não foi possível editar pagamento " + (i + 1).ToString());
                        }
                    }
                    else
                    {
                        sucesso = false;
                        ShowErrorMessage("Data ou Valor de Pagamento inválidos na parcela " + (i + 1).ToString());
                    }
                    i++;
                }
            }

            if (sucesso)
                ShowSuccessMessage("Pagamentos editados com sucesso.");

            FillGrids();
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
    }
}
