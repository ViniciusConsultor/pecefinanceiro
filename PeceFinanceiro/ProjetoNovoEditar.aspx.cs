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

namespace PeceFinanceiro
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        CultureInfo _culture = new CultureInfo("pt-BR");
        Projeto projetoGlobal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                
            }

            string idProjetoSelecionado = "";
            if (!(Request.QueryString["idProjeto"] == null))
            {
                HiddenFieldEditando.Value = "true";
                idProjetoSelecionado = Request.QueryString["idProjeto"].ToString();
                ProjetoNegocio projetoNegocio = new ProjetoNegocio();
                projetoGlobal = projetoNegocio.ObterProjetoPorCodigo(idProjetoSelecionado);
                if (!IsPostBack) FillAllFieldsToEdit(projetoGlobal);
            }
            else
            {
                
            }


            PanelSucesso.Visible = false;
            PanelErro.Visible = false;
        }


        private void FillAllFieldsToEdit(Projeto projeto)
        {
            this.TextBoxCodigoProjeto.Text = projeto.Codigo;
            this.TextBoxNomeProjeto.Text = projeto.Nome;
            this.TextValorProjeto.Text = projeto.Valor.ToString("#0.00").Replace('.', ',');
            this.TextBoxDescricaoProjeto.Text = projeto.Descricao;
            this.TextBoxCodigoProjeto.ReadOnly = true;

            
        }

        private void ShowSuccessMessage(string successMessage)
        {
            PanelErro.Visible = false;
            PanelSucesso.Visible = true;
            MensagemSucesso.Text = successMessage;
        }

        private void ShowErrorMessage(string errorMessage)
        {
            PanelSucesso.Visible = false;
            PanelErro.Visible = true;
            MensagemErro.Text = errorMessage;
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjetosLista.aspx");
        }


        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            bool errorOccured = false;
            string errorMessage = "Ocorreram erros durante o processamento. <ul>";

            Projeto projeto = new Projeto();

            if (this.TextBoxCodigoProjeto.Text.Trim().ToString() != "")
                projeto.Codigo = this.TextBoxCodigoProjeto.Text;
            else
            {
                errorOccured = true;
                errorMessage += "Código do projeto é obrigatório.";
            }

            if (this.TextBoxNomeProjeto.Text.Trim().ToString() != "")
                projeto.Nome = this.TextBoxNomeProjeto.Text;
            else
            {
                errorOccured = true;
                errorMessage += "Nome do projeto é obrigatório.";
            }

            projeto.Descricao = this.TextBoxDescricaoProjeto.Text;

            Double valorProjeto = 0.0;
            if (Double.TryParse(this.TextValorProjeto.Text, NumberStyles.Currency, _culture, out valorProjeto) && valorProjeto > 0)
                projeto.Valor = valorProjeto;
            else
            {
                errorOccured = true;
                errorMessage += "Valor inválido para o valor do projeto.";
            }

            ProjetoNegocio projetoNegocio = new ProjetoNegocio();
            if (errorOccured)
            {
                ShowErrorMessage(errorMessage);
            }
            else
            {

                if (!Boolean.Parse(HiddenFieldEditando.Value))
                {
                    if (projetoNegocio.IncluirProjeto(projeto))
                    {
                        ShowSuccessMessage("Cadastro  do projeto realizado com sucesso. <a href=\"ProjetoNovoEditar.aspx?idProjeto=" + projeto.Codigo + "\">Clique aqui para editar este projeto.</a>");
                        
                    }
                }
                else
                {
                    if (projetoNegocio.AtualizarProjeto(projeto))
                        ShowSuccessMessage("Projeto atualizado com sucesso."); ;
                }
            }
        }
    }
}
