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
using Vsf.Modelo;
using System.Collections.Generic;

namespace PeceFinanceiro
{
    public partial class ListaProjetos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridViewListaProjetos();
        }

        private void LoadGridViewListaProjetos()
        {
            ProjetoNegocio projeto = new ProjetoNegocio();
            List<Projeto> listProjetos = projeto.ObterTodosProjetos();
                     
            GridViewListaProjetos.Columns.Clear();

            GridViewListaProjetos.DataSource = listProjetos;
            GridViewListaProjetos.AutoGenerateColumns = false;

            BoundField bfCodigo = new BoundField();
            bfCodigo.DataField = "Codigo";
            bfCodigo.HeaderText = "Codigo";
            GridViewListaProjetos.Columns.Add(bfCodigo);

            BoundField bfNomeProjeto = new BoundField();
            bfNomeProjeto.DataField = "Nome";
            bfNomeProjeto.HeaderText = "Nome";
            GridViewListaProjetos.Columns.Add(bfNomeProjeto);

            BoundField bfValorProjeto = new BoundField();
            bfValorProjeto.DataField = "Valor";
            bfValorProjeto.HeaderText = "Valor";
            GridViewListaProjetos.Columns.Add(bfValorProjeto);

            CommandField cmdField = new CommandField();
            cmdField.ButtonType = ButtonType.Image;
            cmdField.DeleteImageUrl = "Icons/cross.png";
            cmdField.EditImageUrl = "Icons/page_edit.png";
            cmdField.SelectImageUrl = "Icons/money_add.png";
            cmdField.ShowDeleteButton = true;
            cmdField.ShowEditButton = true;
            cmdField.ShowSelectButton = true;
            cmdField.EditText = "Editar Projeto";
            cmdField.DeleteText = "Remover Projeto";
            
            GridViewListaProjetos.Columns.Add(cmdField);

            GridViewListaProjetos.DataBind();
        }

        protected void GridViewListaProjetos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<Projeto> listProjeto = (List<Projeto>)GridViewListaProjetos.DataSource;
            String IdProjeto = listProjeto[e.NewEditIndex].Codigo;

            Response.Redirect("ProjetoNovoEditar.aspx?idProjeto=" + IdProjeto);
        }

        protected void GridViewListaProjetos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Projeto> listProjeto = (List<Projeto>)GridViewListaProjetos.DataSource;
            String IdProjeto = listProjeto[e.RowIndex].Codigo;

            Response.Redirect("ProjetoNovoEditar.aspx?idProjeto=" + IdProjeto);
        }

        protected void GridViewListaProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //implementar...
        }

        protected void GridViewListaProjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //implementar
        }

    }
}
