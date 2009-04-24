using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vsf.Modelo;
using Vsf.Negocio;

namespace PeceFinanceiro
{
	public partial class NovoUsuario : System.Web.UI.Page
	{
        Usuario _usuario = new Usuario();
        UsuarioNegocio _usuarionegocio = new UsuarioNegocio();
        String _operacao;
		protected void Page_Load(object sender, EventArgs e)
		{
            PanelErro.Visible = false;
            PanelSucesso.Visible = false;
            if (Request.QueryString["login"] != null)
            {
                _operacao = "editar";
                carregarDadosParaEdicao();
            }
            else{
                _operacao = "novo";
            }
		}

        private void carregarDadosParaEdicao()
        {
            if (!Page.IsPostBack)
            {
                _usuario = _usuarionegocio.ConsultarUsuario(Request.QueryString["login"].ToString());
                TextBoxNome.Text = _usuario.Nome;
                TextBoxLogin.Text = _usuario.Login;
                DropDownListTipo.Items.FindByValue(_usuario.Tipo.ToString()).Selected = true;
                TextBoxLogin.Enabled = false;
                Usuario usuario = (Usuario)Session["usuario"];
                if (!usuario.Login.Equals(_usuario.Login))
                {
                    TextBoxNome.Enabled = false;
                    TextBoxSenha.Enabled = false;
                    TextBoxSenhaConfirma.Enabled = false;
                }
                DropDownListTipo.Enabled = usuario.Tipo == 1 ? true : false;//se o usuario é administrador ele pode mudar seu tipo
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            if (Session["login"].Equals(TextBoxLogin.Text))
            {//se o usuario está editando o próprio cadastro, ele veio pelo link "Editar cadastro" e o botão cancelar volta para a página principal
                Response.Redirect("~/RegistroFinanceiroLista.aspx");
            }
            else
            {
                Response.Redirect("~/UsuarioLista.aspx");
            }
        }
        
        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            
            if (!TextBoxLogin.Text.Equals(""))
            {
                if (!TextBoxSenha.Text.Equals("") || _operacao.Equals("editar"))// se é uma edição a senha pode estar em branco
                {
                    if (TextBoxSenha.Text.Equals(TextBoxSenhaConfirma.Text))
                    {
                        if (_operacao.Equals("novo"))
                        {
                            _usuario.Nome = TextBoxNome.Text;
                            _usuario.Login = TextBoxLogin.Text;
                            _usuario.Tipo = Convert.ToInt32(DropDownListTipo.SelectedValue);

                            int novoUsuarioStatus = _usuarionegocio.InserirUsuario(_usuario, TextBoxSenha.Text);
                            if (novoUsuarioStatus == 0)
                            {
                                Response.Redirect("UsuarioLista.aspx?novoUsuario=" + _usuario.Login);
                            }
                            if (novoUsuarioStatus == -1)
                            {
                                ShowErrorMessage("O login: '" + _usuario.Login + "'Já existe. Escolha outro Login");
                                TextBoxLogin.Focus();
                            }
                        }
                        else
                        {//editar
                            _usuario.Nome = TextBoxNome.Text;
                            _usuario.Login = TextBoxLogin.Text;
                            _usuario.Tipo = Convert.ToInt32(DropDownListTipo.SelectedValue);
                            if (_usuarionegocio.AtualizarUsuario(_usuario,TextBoxSenha.Text.ToString()))
                            {
                                Response.Redirect("UsuarioLista.aspx?atualizaçãoUsuario=" + _usuario.Login);
                            }
                            else
                            {
                                ShowErrorMessage("Falha na atualização do usuario!");
                            }
                        }
                    }
                    else
                    {
                        ShowErrorMessage("A confirmação de senha está incorreta");
                    }
                }
                else
                {
                    ShowErrorMessage("A senha não pode ficar em branco");
                }
                  }
            else
            {
                ShowErrorMessage("Preencha o campo Login");
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

