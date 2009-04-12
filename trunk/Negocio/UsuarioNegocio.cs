using System;
using System.Collections.Generic;
using System.Text;
using Vsf.DAL;
using Vsf.Negocio;
using Vsf.Modelo;

namespace Vsf.Negocio
{
    public class UsuarioNegocio
    {
        UsuarioDAO _usuariodao = new UsuarioDAO();

        public Usuario ConsultarUsuario(string login)
        {
            return _usuariodao.ConsultarUsuarioPeloLogin(login);
        }

        public bool Autenticar(string login, string senha)
        {
            return _usuariodao.Autenticar(login,senha);
        }
    }
}
