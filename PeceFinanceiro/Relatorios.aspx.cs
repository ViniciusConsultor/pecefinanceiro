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
using System.Collections.Generic;
using Vsf.Negocio;
using Vsf.Modelo;

namespace PeceFinanceiro
{
    public partial class Relatorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownListRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChamarTipoRelatorio();
        }

        protected void ButtonGerarRelatorio_Click(object sender, EventArgs e)
        {
            ChamarTipoRelatorio();
        }

        private void ChamarTipoRelatorio()
        {
            txtAnoReferencia.Enabled = true;
            txtMesReferencia.Enabled = true;

            if (txtMesReferencia.Text == string.Empty)
            {
                txtMesReferencia.Text = DateTime.Now.Month.ToString();
            }
            if (txtAnoReferencia.Text == string.Empty)
            {
                txtAnoReferencia.Text = DateTime.Now.Year.ToString();
            }
            switch (DropDownListRelatorios.SelectedIndex)
            {
                case 0:
                    RelatorioArrecadacaoMes_load();
                    break;
                case 1:
                    if (txtMesReferencia.Text != string.Empty)
                        RelatorioArrecadacaoPrevistaMes_load();
                    break;
                case 2:
                    RelatorioInadimplentes_load();
                    txtAnoReferencia.Enabled = false;
                    txtMesReferencia.Enabled = false;
                    break;
                case 3:
                    if (txtMesReferencia.Text != string.Empty)
                        RelatorioArrecadacaoDevidaMes_load();
                    break;
            }
        }

        private void RelatorioInadimplentes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();

            lblTituloRelatorio.Text = "Relatório de Alunos Inadimplentes";
            List<AlunoParcela> listAlunoProjeto = relatorioNegocio.ObterAlunosInadimplentes();
            relatorio = relatorioNegocio.ObterRelatorioInadimplentes();
            lbl1.Text = relatorio.ValorTotal.ToString(); 
            textLBL1.Text = "Total: ";
            lbl2.Text = relatorio.NumAlunos.ToString();
            textLBL2.Text = "Número de Alunos: ";
            lbl3.Text = relatorio.MediaDiasAtrasados.ToString();
            textLBL3.Text = "Média de dias Atrasados: ";
            lbl4.Text = relatorio.MaiorAtraso.ToString();
            textLBL4.Text = "Maior Atraso: ";

            GridViewInadimplentes.Columns.Clear();
            GridViewInadimplentes.DataSource = listAlunoProjeto;
            GridViewInadimplentes.AutoGenerateColumns = false;

            BoundField bfNomeAluno = new BoundField();
            bfNomeAluno.DataField = "Nome";
            bfNomeAluno.HeaderText = "Nome do Aluno";
            GridViewInadimplentes.Columns.Add(bfNomeAluno);

            BoundField bfNumeroPece = new BoundField();
            bfNumeroPece.DataField = "NumeroPECE";
            bfNumeroPece.HeaderText = "Número PECE";
            GridViewInadimplentes.Columns.Add(bfNumeroPece);

            BoundField bfParcelaVencida = new BoundField();
            bfParcelaVencida.DataField = "ParcelaVencida";
            bfParcelaVencida.HeaderText = "Parcela Vencida";
            GridViewInadimplentes.Columns.Add(bfParcelaVencida);

            BoundField bfValorDevido = new BoundField();
            bfValorDevido.DataField = "ValorParcela";
            bfValorDevido.HeaderText = "Valor Devido";
            GridViewInadimplentes.Columns.Add(bfValorDevido);

            GridViewInadimplentes.DataBind();
        }
        
        private void RelatorioArrecadacaoMes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();
            int mes = Convert.ToInt32(txtMesReferencia.Text);
            int ano = Convert.ToInt32(txtAnoReferencia.Text);
           
            lblTituloRelatorio.Text = "Relatório Mensal da Arrecadação Confirmada";

            List<Projeto> listProjeto = relatorioNegocio.ObterArrecadacaoMes(mes,ano);
            relatorio = relatorioNegocio.ObterRelatorioArrecadacaoMes(mes,ano);
            lbl1.Text = relatorio.ValorTotal.ToString();
            textLBL1.Text = "Total Arrecadado: ";
            lbl2.Text = relatorio.ValorJuros.ToString();
            textLBL2.Text = "Arrecadação com os juros: ";
            lbl3.Text = relatorio.NumAlunos.ToString();
            textLBL3.Text = "Número de Alunos: ";
            lbl4.Text = relatorio.NumProjetos.ToString();
            textLBL4.Text = "Número de Projetos: ";
            GridViewInadimplentes.Columns.Clear();
            GridViewInadimplentes.DataSource = listProjeto;
            GridViewInadimplentes.AutoGenerateColumns = false;

            BoundField bfNomeProjeto = new BoundField();
            bfNomeProjeto.DataField = "Nome";
            bfNomeProjeto.HeaderText = "Nome do Projeto";
            GridViewInadimplentes.Columns.Add(bfNomeProjeto);

            BoundField bfCodProjeto = new BoundField();
            bfCodProjeto.DataField = "Codigo";
            bfCodProjeto.HeaderText = "Código do Projeto";
            GridViewInadimplentes.Columns.Add(bfCodProjeto);

            BoundField bfValorArrecadado = new BoundField();
            bfValorArrecadado.DataField = "Valor";
            bfValorArrecadado.HeaderText = "Arrecadação no Mês";
            GridViewInadimplentes.Columns.Add(bfValorArrecadado);


            GridViewInadimplentes.DataBind();
        }

        private void RelatorioArrecadacaoPrevistaMes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();
            int mes = Convert.ToInt32(txtMesReferencia.Text);
            int ano = Convert.ToInt32(txtAnoReferencia.Text);

            lblTituloRelatorio.Text = "Relatório de Arrecadação Estimada no Mês";
            List<Projeto> listProjeto = relatorioNegocio.ObterArrecadacaoPrevistaMes(mes,ano);
            relatorio = relatorioNegocio.ObterRelatorioArrecadacaoPrevistaMes(mes,ano);
            lbl1.Text = relatorio.ValorTotal.ToString();
            textLBL1.Text = "Total: ";
            lbl2.Text = relatorio.NumAlunos.ToString();
            textLBL2.Text = "Número de Alunos: ";
            lbl3.Text = relatorio.NumProjetos.ToString();
            textLBL3.Text = "Número de Projetos: ";
            lbl4.Visible = false;
            textLBL4.Visible = false;

            GridViewInadimplentes.Columns.Clear();
            GridViewInadimplentes.DataSource = listProjeto;
            GridViewInadimplentes.AutoGenerateColumns = false;

            BoundField bfNomeProjeto = new BoundField();
            bfNomeProjeto.DataField = "Nome";
            bfNomeProjeto.HeaderText = "Nome do Projeto";
            GridViewInadimplentes.Columns.Add(bfNomeProjeto);

            BoundField bfCodProjeto = new BoundField();
            bfCodProjeto.DataField = "Codigo";
            bfCodProjeto.HeaderText = "Código do Projeto";
            GridViewInadimplentes.Columns.Add(bfCodProjeto);

            BoundField bfValorPrevisto = new BoundField();
            bfValorPrevisto.DataField = "Valor";
            bfValorPrevisto.HeaderText = "Arrecadação Prevista no Mês";
            GridViewInadimplentes.Columns.Add(bfValorPrevisto);


            GridViewInadimplentes.DataBind();
        }

        private void RelatorioArrecadacaoDevidaMes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();
            int mes = Convert.ToInt32(txtMesReferencia.Text);
            int ano = Convert.ToInt32(txtAnoReferencia.Text);

            lblTituloRelatorio.Text = "Relatório Mensal e Contas dos Alunos Inadimplentes";
            List<Projeto> listProjeto = relatorioNegocio.ObterArrecadacaoDevidaMes(mes,ano);
            relatorio = relatorioNegocio.ObterRelatorioArrecadacaoDevidaMes(mes,ano);
            lbl1.Text = relatorio.ValorTotal.ToString();
            textLBL1.Text = "Total: ";
            lbl2.Text = relatorio.NumAlunos.ToString();
            textLBL2.Text = "Número de Alunos: ";
            lbl3.Text = relatorio.NumProjetos.ToString();
            textLBL3.Text = "Número de Projetos: ";
            lbl4.Visible = false;
            textLBL4.Visible = false;
            GridViewInadimplentes.Columns.Clear();
            GridViewInadimplentes.DataSource = listProjeto;
            GridViewInadimplentes.AutoGenerateColumns = false;

            BoundField bfNomeProjeto = new BoundField();
            bfNomeProjeto.DataField = "Nome";
            bfNomeProjeto.HeaderText = "Nome do Projeto";
            GridViewInadimplentes.Columns.Add(bfNomeProjeto);

            BoundField bfCodProjeto = new BoundField();
            bfCodProjeto.DataField = "Codigo";
            bfCodProjeto.HeaderText = "Código do Projeto";
            GridViewInadimplentes.Columns.Add(bfCodProjeto);

            BoundField bfValorDevido = new BoundField();
            bfValorDevido.DataField = "Valor";
            bfValorDevido.HeaderText = "Arrecadação Devida no Mês";
            GridViewInadimplentes.Columns.Add(bfValorDevido);


            GridViewInadimplentes.DataBind();
        }

        protected void GridViewInadimplentes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
