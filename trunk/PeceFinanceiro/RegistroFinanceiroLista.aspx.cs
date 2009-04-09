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

            GridViewListaRegistros.Columns.Clear();

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

            CommandField cmdField = new CommandField();
            cmdField.ButtonType = ButtonType.Image;
            cmdField.DeleteImageUrl = "Icons/cross.png";
            cmdField.EditImageUrl = "Icons/page_edit.png";
            cmdField.SelectImageUrl = "Icons/money_add.png";
            cmdField.ShowDeleteButton = true;
            cmdField.ShowEditButton = true;
            cmdField.ShowSelectButton = true;
            cmdField.EditText = "Editar Registro";
            cmdField.DeleteText = "Remover Regisro";
            cmdField.SelectText = "Registrar Pagamentos";

            GridViewListaRegistros.Columns.Add(cmdField);

            GridViewListaRegistros.DataBind();

        }

        protected void GridViewListaRegistros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<AlunoProjeto> listAlunoProjeto = (List<AlunoProjeto>)GridViewListaRegistros.DataSource;
            Int32 IdAlunoProjeto = listAlunoProjeto[e.NewEditIndex].Id;

            Response.Redirect("RegistroFinanceiroNovoEditar.aspx?idRegistro=" + IdAlunoProjeto);
        }

        protected void GridViewListaRegistros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<AlunoProjeto> listAlunoProjeto = (List<AlunoProjeto>)GridViewListaRegistros.DataSource;
            Int32 IdAlunoProjeto = listAlunoProjeto[e.RowIndex].Id;

            Response.Redirect("RegistroFinanceiroRemover.aspx?idRegistro=" + IdAlunoProjeto);
        }

        protected void GridViewListaRegistros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                List<AlunoProjeto> listAlunoProjeto = (List<AlunoProjeto>)GridViewListaRegistros.DataSource;
                Int32 IdAlunoProjeto = listAlunoProjeto[Int32.Parse(e.CommandArgument.ToString())].Id;

                Response.Redirect("RegistroPagamentos.aspx?idRegistro=" + IdAlunoProjeto);
            }
        }

        protected void GridViewListaRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

