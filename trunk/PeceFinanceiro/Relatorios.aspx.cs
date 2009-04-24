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
            bttImpressao.Attributes.Add("onclick", "pagePrint()");
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
            mnMesReferencia.Enabled = true;

            PanelSucesso.Visible = false;
            lbl1.Text = string.Empty;
            lbl2.Text = string.Empty;
            lbl3.Text = string.Empty;
            lbl4.Text = string.Empty;
            lblDataAtual.Text = string.Empty;
            lblTituloRelatorio.Text = string.Empty;
            GridViewInadimplentes.Visible = false;
            bttImpressao.Visible = false;

            if (mnMesReferencia.Text == string.Empty)
            {
                mnMesReferencia.Text = DateTime.Now.Month.ToString();
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
                    if (mnMesReferencia.Text != string.Empty)
                        RelatorioArrecadacaoPrevistaMes_load();
                    break;
                case 2:
                    RelatorioInadimplentes_load();
                    txtAnoReferencia.Enabled = false;
                    mnMesReferencia.Enabled = false;
                    break;
                case 3:
                    if (mnMesReferencia.Text != string.Empty)
                        RelatorioArrecadacaoDevidaMes_load();
                    break;
            }
            
            
        }

        private void RelatorioInadimplentes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();

            lblTituloRelatorio.Text = "Relatório de Alunos Inadimplentes";
            lblDataAtual.Text = DateTime.Now.ToLongDateString();
            GridViewInadimplentes.Columns.Clear();

            relatorio = relatorioNegocio.ObterRelatorioInadimplentes();
            if (relatorio.NumProjetos > 0 || relatorio.NumAlunos >0)
            {
                List<AlunoParcela> listAlunoProjeto = relatorioNegocio.ObterAlunosInadimplentes();
                GridViewInadimplentes.Visible = true;
                bttImpressao.Visible = true;
                lbl1.Text = "R$ " + relatorio.ValorTotal.ToString();
                textLBL1.Text = "Total: ";
                lbl2.Text = relatorio.NumAlunos.ToString() + " alunos";
                textLBL2.Text = "Número de Alunos: ";
                lbl3.Text = relatorio.MediaDiasAtrasados.ToString() + " dias";
                textLBL3.Text = "Média de dias Atrasados: ";
                lbl4.Text = relatorio.MaiorAtraso.ToString() + " dias";
                textLBL4.Text = "Maior Atraso: ";
                textLBL4.Visible = true;
                lbl4.Visible = true;

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
                bfParcelaVencida.HtmlEncode = false;
                bfParcelaVencida.DataField = "ParcelaVencida";
                bfParcelaVencida.DataFormatString = "{0:d}";
                bfParcelaVencida.HeaderText = "Parcela Vencida";
                GridViewInadimplentes.Columns.Add(bfParcelaVencida);

                BoundField bfValorDevido = new BoundField();
                bfValorDevido.DataFormatString = "R$ {0:F2}";
                bfValorDevido.DataField = "ValorParcela";
                bfValorDevido.HeaderText = "Valor Devido";
                GridViewInadimplentes.Columns.Add(bfValorDevido);

                GridViewInadimplentes.DataBind();
            }
            else
            {
                PanelSucesso.Visible = true;
            }
        }
        
        private void RelatorioArrecadacaoMes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();
            int mes = Convert.ToInt32(mnMesReferencia.Text);
            int ano = Convert.ToInt32(txtAnoReferencia.Text);
           
            lblTituloRelatorio.Text = "Relatório Mensal da Arrecadação Confirmada";
            lblDataAtual.Text = DateTime.Now.ToLongDateString();
            GridViewInadimplentes.Columns.Clear();

            relatorio = relatorioNegocio.ObterRelatorioArrecadacaoMes(mes,ano);
            if (relatorio.NumProjetos > 0 || relatorio.NumAlunos > 0)
            {
                GridViewInadimplentes.Visible = true;
                bttImpressao.Visible = true;
                List<Projeto> listProjeto = relatorioNegocio.ObterArrecadacaoMes(mes, ano);
                lbl1.Text = "R$ " + relatorio.ValorTotal.ToString();
                textLBL1.Text = "Total Arrecadado: ";
                lbl2.Text = "R$ " + relatorio.ValorJuros.ToString();
                textLBL2.Text = "Arrecadação com os juros: ";
                lbl3.Text = relatorio.NumAlunos.ToString() + " alunos";
                textLBL3.Text = "Número de Alunos: ";
                lbl4.Text = relatorio.NumProjetos.ToString() + " projetos";
                textLBL4.Text = "Número de Projetos: ";
                textLBL4.Visible = true;
                lbl4.Visible = true;

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
                bfValorArrecadado.DataFormatString = "R$ {0:F2}";
                bfValorArrecadado.HeaderText = "Arrecadação no Mês";
                GridViewInadimplentes.Columns.Add(bfValorArrecadado);

                GridViewInadimplentes.DataBind();
            }
            else
            {
                PanelSucesso.Visible = true;
            }
        }

        private void RelatorioArrecadacaoPrevistaMes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();
            int mes = Convert.ToInt32(mnMesReferencia.Text);
            int ano = Convert.ToInt32(txtAnoReferencia.Text);

            lblTituloRelatorio.Text = "Relatório de Arrecadação Estimada no Mês";
            lblDataAtual.Text = DateTime.Now.ToLongDateString();
            GridViewInadimplentes.Columns.Clear();

            relatorio = relatorioNegocio.ObterRelatorioArrecadacaoPrevistaMes(mes,ano);
            if (relatorio.NumProjetos > 0 || relatorio.NumAlunos > 0)
            {
                GridViewInadimplentes.Visible = true;
                bttImpressao.Visible = true;
                List<Projeto> listProjeto = relatorioNegocio.ObterArrecadacaoPrevistaMes(mes, ano);
                lbl1.Text = "R$ " + relatorio.ValorTotal.ToString();
                textLBL1.Text = "Total: ";
                lbl2.Text = relatorio.NumAlunos.ToString() + " alunos";
                textLBL2.Text = "Número de Alunos: ";
                lbl3.Text = relatorio.NumProjetos.ToString()+" projetos";
                textLBL3.Text = "Número de Projetos: ";
                lbl4.Visible = false;
                textLBL4.Visible = false;

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
                bfValorPrevisto.DataFormatString = "R$ {0:F2}";
                bfValorPrevisto.HeaderText = "Arrecadação Prevista no Mês";
                GridViewInadimplentes.Columns.Add(bfValorPrevisto);


                GridViewInadimplentes.DataBind();
            }
            else
            {
                PanelSucesso.Visible = true;
            }
        }

        private void RelatorioArrecadacaoDevidaMes_load()
        {
            RelatorioNegocio relatorioNegocio = new RelatorioNegocio();
            Relatorio relatorio = new Relatorio();
            int mes = Convert.ToInt32(mnMesReferencia.Text);
            int ano = Convert.ToInt32(txtAnoReferencia.Text);

            lblTituloRelatorio.Text = "Relatório Mensal e Contas dos Alunos Inadimplentes";
            lblDataAtual.Text = DateTime.Now.ToLongDateString();
            GridViewInadimplentes.Columns.Clear();

            relatorio = relatorioNegocio.ObterRelatorioArrecadacaoDevidaMes(mes, ano);
            if (relatorio.NumProjetos > 0 || relatorio.NumAlunos > 0)
            {
                GridViewInadimplentes.Visible = true;
                bttImpressao.Visible = true;
                List<Projeto> listProjeto = relatorioNegocio.ObterArrecadacaoDevidaMes(mes, ano);
                lbl1.Text = "R$ " + relatorio.ValorTotal.ToString();
                textLBL1.Text = "Total: ";
                lbl2.Text = relatorio.NumAlunos.ToString()+ " alunos";
                textLBL2.Text = "Número de Alunos: ";
                lbl3.Text = relatorio.NumProjetos.ToString()+" projetos";
                textLBL3.Text = "Número de Projetos: ";
                lbl4.Visible = false;
                textLBL4.Visible = false;

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
                bfValorDevido.DataFormatString = "R$ {0:F2}";
                bfValorDevido.HeaderText = "Arrecadação Devida no Mês";
                GridViewInadimplentes.Columns.Add(bfValorDevido);


                GridViewInadimplentes.DataBind();
            }
            else
            {
                PanelSucesso.Visible = true;
            }
        }

        protected void GridViewInadimplentes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonGerarImpressao_Click(object sender, EventArgs e)
        {

        }


    }
}
