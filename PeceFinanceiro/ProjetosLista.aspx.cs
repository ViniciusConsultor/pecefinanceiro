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
        ProjetoNegocio projetoNegocio = new ProjetoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridViewListaProjetos();
            PanelSucesso.Visible = false;
            PanelErro.Visible = false;
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
            cmdField.ShowDeleteButton = true;
            cmdField.ShowEditButton = true;
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
            ProjetoNegocio projeto = new ProjetoNegocio();
            ProjetoNegocio projetoNegocio = new ProjetoNegocio();
            if (projetoNegocio.ProjetoDeletarOK(IdProjeto))
            {
                if (projetoNegocio.DeletarProjeto(IdProjeto))
                {
                    ShowSuccessMessage("Projeto deletado com sucesso.");
                    LoadGridViewListaProjetos();
                }
                else
                {
                    ShowSuccessMessage("Ocorreu um erro no processo.");
                }
                
            }
            else 
            {
                ShowErrorMessage("Este Projeto não pode ser deletado.");
            }
        }

        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            List<Projeto> listaProjeto = new List<Projeto>();
            listaProjeto = projetoNegocio.BuscaProjetosPeloCodigoENome(this.TextBoxBuscaCodigoProjeto.Text, this.TextBoxBuscaNomeProjeto.Text);
            //GridViewListaUsuarios.Columns.Clear();
            GridViewListaProjetos.DataSource = listaProjeto;
            GridViewListaProjetos.DataBind();
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

    }
}
