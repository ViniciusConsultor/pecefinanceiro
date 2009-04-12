using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Common;
using Vsf.Common.Database;
using Vsf.Modelo;
using System.Data.SqlClient;

namespace Vsf.DAL
{
    public class UsuarioDAO
    {
        public bool Autenticar(string login, string senha)
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@login", login));
                parameters.Add(new SqlParameter("@senha", senha));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT NomeAcesso, SenhaAcesso FROM Usuario");
                query.Append(" WHERE NomeAcesso = @login ");
                query.Append(" AND SenhaAcesso = @senha ");

                
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);


                if (reader.Read())
                {
                    if (login.Equals(reader["NomeAcesso"]) && senha.Equals(reader["SenhaAcesso"])) ;
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOUsuario.Autenticar(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            
        }

        public Usuario ConsultarUsuarioPeloLogin(string login)
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@login", login));
               
                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Usuario");
                query.Append(" WHERE NomeAcesso = @login ");
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                
                if (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Login = (reader["NomeAcesso"] != DBNull.Value) ? Convert.ToString(reader["NomeAcesso"]) : String.Empty;
                    usuario.IdUsuario = (reader["IdUsuario"] != DBNull.Value) ? Convert.ToInt32(reader["IdUsuario"]) : 0;
                    usuario.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;

                    return usuario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOUsuario.ConsultarUsuarioPeloLogin(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
        }
    }
}
