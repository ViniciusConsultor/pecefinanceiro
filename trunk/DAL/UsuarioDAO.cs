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
                    usuario.Tipo = (reader["Nome"] != DBNull.Value) ? Convert.ToInt32(reader["TipoUsuario"]) : 0;

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

        public bool InserirUsuario(Usuario usuario, string senha)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Nome", usuario.Nome));
                parameters.Add(new SqlParameter("@NomeAcesso", usuario.Login));
                parameters.Add(new SqlParameter("@SenhaAcesso", senha));
                parameters.Add(new SqlParameter("@TipoUsuario", usuario.Tipo));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Usuario");
                query.Append(" (Nome, NomeAcesso, SenhaAcesso, TipoUsuario)");
                query.Append(" VALUES (@Nome, @NomeAcesso, @SenhaAcesso, @TipoUsuario)");
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOUsuario.InserirUsuario(Aluno): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public List<Usuario> ObterTodosUsuarios()
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            List<Usuario> listusuarios = new List<Usuario>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Usuario");
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Login = (reader["NomeAcesso"] != DBNull.Value) ? Convert.ToString(reader["NomeAcesso"]) : String.Empty;
                    usuario.IdUsuario = (reader["IdUsuario"] != DBNull.Value) ? Convert.ToInt32(reader["IdUsuario"]) : 0;
                    usuario.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    usuario.Tipo = (reader["Nome"] != DBNull.Value) ? Convert.ToInt32(reader["TipoUsuario"]) : 0;
                    listusuarios.Add(usuario);
                 }
                
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

            return listusuarios;
        }

        public bool RemoverUsuario(String usuario)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@NomeAcesso", usuario));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("DELETE FROM Usuario");
                query.Append(" WHERE NomeAcesso=@NomeAcesso ");
                
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOUsuario.RemoverUsuario(Usuario): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public bool AtualizarUsuario(Usuario usuario, String senha)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Nome", usuario.Nome));
                parameters.Add(new SqlParameter("@TipoUsuario", usuario.Tipo));
                if (!senha.Equals(""))
                {
                    parameters.Add(new SqlParameter("@SenhaAcesso", senha));
                }
                parameters.Add(new SqlParameter("@NomeAcesso", usuario.Login));
                
                db.AbreConexao();

                StringBuilder query = new StringBuilder("UPDATE Usuario");
                query.Append(" set Nome = @Nome, TipoUsuario=@TipoUsuario ");
                if (!senha.Equals(""))
                {
                    query.Append(" , SenhaAcesso=@SenhaAcesso ");
                }
                query.Append(" WHERE (NomeAcesso=@NomeAcesso)");
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOUsuario.InserirUsuario(Aluno): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public List<Usuario> BuscaUsuariosPeloLoginENome(string Login, string Nome)
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            List<Usuario> listausuarios = new List<Usuario>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (!Login.Equals(""))
                {
                    parameters.Add(new SqlParameter("@login", "%" + Login + "%"));
                }
                if (!Nome.Equals(""))
                {
                    parameters.Add(new SqlParameter("@Nome", "%" + Nome + "%"));
                }

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Usuario");
                query.Append(" WHERE ");
                if (!Login.Equals(""))
                {
                    query.Append(" NomeAcesso LIKE @login ");
                }
                if (!Login.Equals("") && !Nome.Equals(""))
                {
                    query.Append(" OR ");
                }
                if (!Nome.Equals(""))
                {
                    query.Append(" Nome LIKE @Nome ");
                }
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Login = (reader["NomeAcesso"] != DBNull.Value) ? Convert.ToString(reader["NomeAcesso"]) : String.Empty;
                    usuario.IdUsuario = (reader["IdUsuario"] != DBNull.Value) ? Convert.ToInt32(reader["IdUsuario"]) : 0;
                    usuario.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    usuario.Tipo = (reader["Nome"] != DBNull.Value) ? Convert.ToInt32(reader["TipoUsuario"]) : 0;
                    listausuarios.Add(usuario);
                    
                }
                return listausuarios;
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
