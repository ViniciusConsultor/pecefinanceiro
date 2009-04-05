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
    public partial class ListaCadastros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegistroFinanceiroNegocio registroNegocio = new RegistroFinanceiroNegocio();
            List<RegistroFinanceiro> listRegistros = registroNegocio.ObterTodosRegistros();

            List<AlunoProjeto> listAlunoProjeto = new List<AlunoProjeto>();

            foreach (RegistroFinanceiro reg in listRegistros)
                listAlunoProjeto.Add(reg.AlunoProjeto);

            GridViewListaRegistros.DataSource = listAlunoProjeto;
            GridViewListaRegistros.AutoGenerateColumns = false;

            BoundField bfNomeAluno = new BoundField();
            bfNomeAluno.DataField = "NomeAluno";
            bfNomeAluno.HeaderText = "Aluno";
            GridViewListaRegistros.Columns.Add(bfNomeAluno);

            BoundField bfProjeto = new BoundField();
            bfProjeto.DataField = "NomeProjeto";
            bfProjeto.HeaderText = "Projeto";
            GridViewListaRegistros.Columns.Add(bfProjeto);

            BoundField bfStatus = new BoundField();
            bfStatus.DataField = "Status";
            bfStatus.HeaderText = "Status";
            GridViewListaRegistros.Columns.Add(bfStatus);

            GridViewListaRegistros.DataBind();

        }
    }
}
