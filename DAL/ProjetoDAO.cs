using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Common.Database;
using System.Data.SqlClient;
using Vsf.Modelo;
using Vsf.Common;
using System.Threading;

namespace Vsf.DAL
{
    public static class ProjetoDAO
    {
        public static List<Projeto> ObterTodosProjetos()
        {
            List<Projeto> listProjeto = new List<Projeto>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                db.AbreConexao();
                
                StringBuilder query =  new StringBuilder("SELECT * FROM Projeto");
                
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    Projeto projeto = new Projeto();
                    projeto.Codigo = (reader["CodigoProjeto"] != DBNull.Value) ? Convert.ToString(reader["CodigoProjeto"]) : String.Empty;
                    projeto.Descricao = (reader["Descricao"] != DBNull.Value) ? Convert.ToString(reader["Descricao"]) : String.Empty; ;
                    projeto.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    projeto.Valor = (reader["ValorProjeto"] != DBNull.Value) ? Convert.ToDouble(reader["ValorProjeto"]) : 0.0;
                    projeto.Alunos = (new AlunoDAO()).ObterAlunosPorProjeto(projeto);

                    listProjeto.Add(projeto);
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.ObterTodosProjetos(): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listProjeto;
        }

        public static Projeto ObterProjetoPorCodigo(string codigoProjeto)
        {
            Projeto projeto = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", codigoProjeto));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Projeto");
                query.Append(" WHERE CodigoProjeto = @codigoProjeto");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                if (reader.Read())
                {
                    projeto = new Projeto();
                    projeto.Codigo = (reader["CodigoProjeto"] != DBNull.Value) ? Convert.ToString(reader["CodigoProjeto"]) : String.Empty;
                    projeto.Descricao = (reader["Descricao"] != DBNull.Value) ? Convert.ToString(reader["Descricao"]) : String.Empty; ;
                    projeto.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    projeto.Valor = (reader["ValorProjeto"] != DBNull.Value) ? Convert.ToDouble(reader["ValorProjeto"]) : 0.0;
                    projeto.Alunos = (new AlunoDAO()).ObterAlunosPorProjeto(projeto);

                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.ObterProjetoPorCodigo(codigoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return projeto;
        }

        public static List<Projeto> ObterProjetosDoAluno(int codigoPece)
        {
            List<Projeto> listProjeto = new List<Projeto>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoPece", codigoPece));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Projeto ");
                query.Append(" WHERE CodigoProjeto in ");
                query.Append(" (SELECT IdProjeto FROM Matricula ");
                query.Append(" WHERE IdAluno = @codigoPece) ");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                   listProjeto.Add(new Projeto(
                        (reader["CodigoProjeto"] != DBNull.Value) ? Convert.ToString(reader["CodigoProjeto"]) : String.Empty,
                        (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty,
                        (reader["Descricao"] != DBNull.Value) ? Convert.ToString(reader["Descricao"]) : String.Empty,
                        (reader["ValorProjeto"] != DBNull.Value) ? Convert.ToDouble(reader["ValorProjeto"]) : 0.0
                        ));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOProjeto.ObterProjetoPorCodigo(codigoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listProjeto;
        }

        public static bool DeletarProjeto(String codigoProjeto)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CodigoProjeto", codigoProjeto));
                
                db.AbreConexao();

                StringBuilder query = new StringBuilder("DELETE PROJETO");
                query.Append(" WHERE CodigoProjeto=@CodigoProjeto");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.DeletarProjeto(codigoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }

            Logger.Registrar(1, "Projeto deletado com número de projeto " + codigoProjeto + ".");

            return (affected > 0);
        }

        public static bool IncluirProjeto(Projeto novoProjeto)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", novoProjeto.Codigo));
                parameters.Add(new SqlParameter("@nome", novoProjeto.Nome));
                parameters.Add(new SqlParameter("@descricao", novoProjeto.Descricao));
                parameters.Add(new SqlParameter("@valorProjeto", novoProjeto.Valor));
                   
                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO PROJETO");
                query.Append(" (CodigoProjeto, Nome, Descricao, ValorProjeto) ");
                query.Append(" VALUES (@codigoProjeto, @nome, @descricao, @valorProjeto)");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.IncluirProjeto(novoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }

            Logger.Registrar(1, "Projeto inserido com número de projeto " + novoProjeto.Codigo + ".");

            return (affected > 0);
        }

        public static bool AtualizarProjeto(Projeto atualizarProjeto)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CodigoProjeto", atualizarProjeto.Codigo));
                parameters.Add(new SqlParameter("@nome", atualizarProjeto.Nome));
                parameters.Add(new SqlParameter("@descricao", atualizarProjeto.Descricao));
                parameters.Add(new SqlParameter("@valorProjeto", atualizarProjeto.Valor));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("UPDATE PROJETO");
                query.Append(" SET Nome=@nome,Descricao=@descricao,ValorProjeto=@valorProjeto");
                query.Append(" WHERE CodigoProjeto=@CodigoProjeto");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.AtualizarProjeto(atualizarProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }

            Logger.Registrar(1, "Projeto atualizado com número de projeto " + atualizarProjeto.Codigo + ".");

            return (affected > 0);
        }

        public static List<Projeto> BuscaProjetosPeloCodigoENome(string Codigo, string Nome)
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            List<Projeto> listaProjetos = new List<Projeto>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (!Codigo.Equals(""))
                {
                    parameters.Add(new SqlParameter("@codigo", "%" + Codigo + "%"));
                }
                if (!Nome.Equals(""))
                {
                    parameters.Add(new SqlParameter("@Nome", "%" + Nome + "%"));
                }

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM PROJETO");
                query.Append(" WHERE ");
                if (!Codigo.Equals(""))
                {
                    query.Append(" CodigoProjeto LIKE @codigo ");
                }
                if (!Codigo.Equals("") && !Nome.Equals(""))
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
                    Projeto projeto = new Projeto();
                    projeto.Codigo = (reader["CodigoProjeto"] != DBNull.Value) ? Convert.ToString(reader["CodigoProjeto"]) : String.Empty;
                    projeto.Valor = (reader["ValorProjeto"] != DBNull.Value) ? Convert.ToDouble(reader["ValorProjeto"]) : 0.0;
                    projeto.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    projeto.Descricao = (reader["Descricao"] != DBNull.Value) ? Convert.ToString(reader["Descricao"]) : String.Empty;
                    listaProjetos.Add(projeto);

                }
                return listaProjetos;
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.BuscaProjetosPeloCodigoENome(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
        } 

        /// <summary>
        /// Este Método retorna os Projetos em que um aluno pode se matricular
        /// </summary>
        /// <param name="codigoPece"></param>
        /// <returns></returns>
        public static List<Projeto> ObterProjetosDisponiveisAoAluno(int codigoPece)
        {
            List<Projeto> listProjeto = new List<Projeto>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoPece", codigoPece));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Projeto ");
                query.Append(" WHERE CodigoProjeto not in ");
                query.Append(" (SELECT IdProjeto FROM Matricula ");
                query.Append(" WHERE IdAluno = @codigoPece) ");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    listProjeto.Add(new Projeto(
                         (reader["CodigoProjeto"] != DBNull.Value) ? Convert.ToString(reader["CodigoProjeto"]) : String.Empty,
                         (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty,
                         (reader["Descricao"] != DBNull.Value) ? Convert.ToString(reader["Descricao"]) : String.Empty,
                         (reader["ValorProjeto"] != DBNull.Value) ? Convert.ToDouble(reader["ValorProjeto"]) : 0.0
                         ));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOProjeto.ObterProjetoPorCodigo(codigoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listProjeto;
        }

        public static bool ProjetoDeletarOK(string codigoProjeto)
        {

            int affected = 1;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", codigoProjeto));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT Aluno.* FROM Aluno");
                query.Append(" INNER JOIN Matricula ON Aluno.NumeroPece = Matricula.IdAluno");
                query.Append(" WHERE IdProjeto = @codigoProjeto");
                
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                
                if (reader.Read())
                {
                    affected = 0;  
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.ProjetoDeletarOK(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }

            
            return (affected > 0);

        }
    }
}
