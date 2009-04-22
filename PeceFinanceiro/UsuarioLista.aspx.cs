using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vsf.Negocio;
using Vsf.Modelo;

namespace PeceFinanceiro
{
    public partial class UsuarioLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridViewListaUsuarios();


        }

        private void LoadGridViewListaUsuarios()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> listUsuarios = usuarioNegocio.ObterTodosUsuarios();
           

            GridViewListaUsuarios.Columns.Clear();

            GridViewListaUsuarios.DataSource = listUsuarios;
            GridViewListaUsuarios.AutoGenerateColumns = false;

            BoundField bfNomeUsuario = new BoundField();
            bfNomeUsuario.DataField = "Nome";
            bfNomeUsuario.HeaderText = "Nome";
            GridViewListaUsuarios.Columns.Add(bfNomeUsuario);

            BoundField bfLogin = new BoundField();
            bfLogin.DataField = "Login";
            bfLogin.HeaderText = "Login";
            GridViewListaUsuarios.Columns.Add(bfLogin);

            CommandField cmdField = new CommandField();
            cmdField.ButtonType = ButtonType.Image;
            cmdField.DeleteImageUrl = "Icons/cross.png";
            cmdField.EditImageUrl = "Icons/page_edit.png";
            //cmdField.SelectImageUrl = "Icons/money_add.png";
            cmdField.ShowDeleteButton = true;
            cmdField.ShowEditButton = true;
            //cmdField.ShowSelectButton = true;
            cmdField.EditText = "Editar Usuario";
            cmdField.DeleteText = "Remover Usuario";

            GridViewListaUsuarios.Columns.Add(cmdField);
            GridViewListaUsuarios.DataBind();
            
        }

        protected void GridViewListaRegistros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<Usuario> listUsuario = (List<Usuario>)GridViewListaUsuarios.DataSource;
            String UsuarioLogin = listUsuario[e.NewEditIndex].Login;

            Response.Redirect("UsuarioNovoEditar.aspx?login=" + UsuarioLogin);
        }

        protected void GridViewListaRegistros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Usuario> listUsuario = (List<Usuario>)GridViewListaUsuarios.DataSource;
            String usuarioLogin = listUsuario[e.RowIndex].Nome;

            Response.Redirect("UsuarioRemover.aspx?login=" + usuarioLogin);
            
        }

        protected void GridViewListaRegistros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
               // List<AlunoProjeto> listAlunoProjeto = (List<AlunoProjeto>)GridViewListaRegistros.DataSource;
               // int index = Int32.Parse(Convert.ToString(e.CommandArgument));
               // Int32 IdAlunoProjeto = listAlunoProjeto[index].Id;

               // Response.Redirect("RegistroPagamentos.aspx?idMatricula=" + IdAlunoProjeto);
            }
        }

        protected void GridViewListaRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
