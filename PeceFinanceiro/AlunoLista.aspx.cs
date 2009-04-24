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
using Vsf.Modelo;
using Vsf.Negocio;
using System.Collections.Generic;

namespace PeceFinanceiro
{
    public partial class ListaAlunos : System.Web.UI.Page
    {
        AlunoNegocio _alunoNegocio = new AlunoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelErro.Visible = false;
            PanelSucesso.Visible = false;
            LoadGridViewListaAlunos();

        }

        private void LoadGridViewListaAlunos()
        {
            AlunoNegocio alunoNegocio = new AlunoNegocio();
            List<Aluno> listAlunos = alunoNegocio.ObterTodosAlunos();

            
            GridViewListaAlunos.Columns.Clear();

            GridViewListaAlunos.DataSource = listAlunos;
            GridViewListaAlunos.AutoGenerateColumns = false;

            BoundField bfNumeroPece = new BoundField();
            bfNumeroPece.DataField = "NumeroPece";
            bfNumeroPece.HeaderText = "Número Pece";
            GridViewListaAlunos.Columns.Add(bfNumeroPece);

            BoundField bfNome = new BoundField();
            bfNome.DataField = "Nome";
            bfNome.HeaderText = "Nome";
            GridViewListaAlunos.Columns.Add(bfNome);

            CommandField cmdField = new CommandField();
            cmdField.ButtonType = ButtonType.Image;
            cmdField.DeleteImageUrl = "Icons/cross.png";
            cmdField.EditImageUrl = "Icons/page_edit.png";
            cmdField.SelectImageUrl = "Icons/money_add.png";
            cmdField.ShowDeleteButton = true;
            cmdField.ShowEditButton = true;
            cmdField.ShowSelectButton = false;
            cmdField.EditText = "Editar Usuario";
            cmdField.DeleteText = "Remover Usuario";

            GridViewListaAlunos.Columns.Add(cmdField);
            GridViewListaAlunos.DataBind();

        }

        protected void GridViewListaAlunos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<Aluno> listAluno = (List<Aluno>)GridViewListaAlunos.DataSource;
            int AlunoNumeroPeceLogin = listAluno[e.NewEditIndex].NumeroPece;
            
            Response.Redirect("AlunoNovoEditar.aspx?numeropece=" + AlunoNumeroPeceLogin);
        }

        protected void GridViewListaAlunos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Aluno> listUsuario = (List<Aluno>)GridViewListaAlunos.DataSource;
            Int32 AlunoNumeroPece = listUsuario[e.RowIndex].NumeroPece;
            if (!_alunoNegocio.AlunoPossuiMatricula(AlunoNumeroPece))
            {//Um aluno só pode ser excluído se não possui nenhuma matrícula ativa
                Response.Redirect("AlunoRemover.aspx?numeropece=" + AlunoNumeroPece);
            }
            else
            {
                ShowErrorMessage("Remova as matrículas deste aluno antes de excluí-lo");
            }

        }

        
        protected void GridViewListaAlunos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // List<AlunoProjeto> listAlunoProjeto = (List<AlunoProjeto>)GridViewListaRegistros.DataSource;
                // int index = Int32.Parse(Convert.ToString(e.CommandArgument));
                // Int32 IdAlunoProjeto = listAlunoProjeto[index].Id;

                // Response.Redirect("RegistroPagamentos.aspx?idMatricula=" + IdAlunoProjeto);
            }
        }

        protected void GridViewListaAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            if (!(TextBoxBuscaNumeroPece.Text.Equals("") && TextBoxBuscaNomeAluno.Text.Equals("")))
            {
                List<Aluno> listaAlunosBusca = new List<Aluno>();
                listaAlunosBusca = _alunoNegocio.BuscaAlunoPeloNumeroPeceENome(TextBoxBuscaNumeroPece.Text, TextBoxBuscaNomeAluno.Text);
                //GridViewListaUsuarios.Columns.Clear();
                GridViewListaAlunos.DataSource = listaAlunosBusca;
                GridViewListaAlunos.DataBind();
            }
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
