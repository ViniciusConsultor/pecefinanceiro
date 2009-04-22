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
		protected void Page_Load(object sender, EventArgs e)
		{
            PanelErro.Visible = false;
            PanelSucesso.Visible = false;
		}

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        
        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            
            if (!TextBoxLogin.Text.Equals(""))
            {
                if (!TextBoxSenha.Text.Equals(""))
                {
                    if (TextBoxSenha.Text.Equals(TextBoxSenhaConfirma.Text))
                    {
                        Usuario novousuario = new Usuario();
                        novousuario.Nome = TextBoxNome.Text;
                        novousuario.Login = TextBoxLogin.Text;
                        novousuario.Tipo = Convert.ToInt32(DropDownListTipo.SelectedValue);
                        UsuarioNegocio usuarionegocio = new UsuarioNegocio();
                        int novoUsuarioStatus = usuarionegocio.InserirUsuario(novousuario, TextBoxSenha.Text);
                        if (novoUsuarioStatus == 0)
                        {
                            Response.Redirect("UsuarioLista.aspx?novoUsuario=" + novousuario.Login);
                        }
                        if (novoUsuarioStatus == -1)
                        {
                            ShowErrorMessage("O login: '" + novousuario.Login + "'Já existe. Escolha outro Login");
                            TextBoxLogin.Focus();
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

