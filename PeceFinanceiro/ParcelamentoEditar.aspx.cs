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
using Vsf.Negocio;
using System.Collections.Generic;
using Vsf.Modelo;
using System.Globalization;

namespace PeceFinanceiro
{
    public partial class EditarParcelamento : System.Web.UI.Page
    {
        CultureInfo _culture = new CultureInfo("pt-BR");
        Int32 _idRegistro = -1;
        Int32 _idMatricula = -1;
        RegistroFinanceiro registro;
        protected void Page_Load(object sender, EventArgs e)
        {
            _idRegistro = Convert.ToInt32(Request.QueryString["idRegistro"]);
            this.Culture = "pt-BR";

            RegistroFinanceiroNegocio registroNegocio = new RegistroFinanceiroNegocio();
            registro = registroNegocio.ObterRegistroPorId(_idRegistro);
            _idMatricula = registro.AlunoProjeto.Id;

            if (!IsPostBack)
            {
                FillGrids();
                FillSummary();
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
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroFinanceiroNovoEditar.aspx?idMatricula=" + _idMatricula);
        }

    }
}
