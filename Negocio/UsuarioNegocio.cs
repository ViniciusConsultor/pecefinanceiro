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

        public List<Usuario> ObterTodosUsuarios()
        {
            return _usuariodao.ObterTodosUsuarios();
        }
        public Usuario ConsultarUsuario(string login)
        {
            return _usuariodao.ConsultarUsuarioPeloLogin(login);
        }

        public bool Autenticar(string login, string senha)
        {
            return _usuariodao.Autenticar(login,senha);
        }
        /// <summary>
        /// Tenta inserir um usuário novo<br></br>
        /// Retorna "0" se inseriu com sucesso<br></br>
        /// Retorna "-1" se o usuario já existe<br></br>
        /// Retorna "-2" se houve erro na inserção<br></br>
        /// 
        /// </summary>
        /// <param name="novousuario"></param>
        /// <returns>
        /// 
        /// </returns>
        public int InserirUsuario(Usuario novousuario, String senha)
        {
            //verifica se o login já nao existe
            if (_usuariodao.ConsultarUsuarioPeloLogin(novousuario.Login) != null)
            {
                return -1;
            }
            else
            {
                if (_usuariodao.InserirUsuario(novousuario, senha))
                {
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
                        
        }

        public bool RemoverUsuario(String login)
        {
            return _usuariodao.RemoverUsuario(login); ;
        }

        public bool AtualizarUsuario(Usuario _usuario, String senha)
        {
            return _usuariodao.AtualizarUsuario(_usuario,senha);
        }

        public List<Usuario> BuscaUsuariosPeloLoginENome(String Login, String Nome)
        {
            return _usuariodao.BuscaUsuariosPeloLoginENome(Login, Nome);
            
        }
    }
}
