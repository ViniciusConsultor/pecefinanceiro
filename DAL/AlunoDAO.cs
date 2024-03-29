﻿using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using Vsf.Common.Database;
using System.Data.SqlClient;
using Vsf.Common;

namespace Vsf.DAL
{
    public class AlunoDAO
    {
        public List<Aluno> ObterAlunosPorProjeto(Projeto projeto)
        {
            List<Aluno> listAlunos = new List<Aluno>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", projeto.Codigo));

                db.AbreConexao();
                
                StringBuilder query =  new StringBuilder("SELECT Aluno.* FROM Aluno");
                query.Append(" INNER JOIN Matricula ON Aluno.NumeroPece = Matricula.IdAluno");
                query.Append(" WHERE IdProjeto = @codigoProjeto");
                
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.NumeroPece = (reader["NumeroPece"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroPece"]) : 0;
                    aluno.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    aluno.Endereco = (reader["Endereco"] != DBNull.Value) ? Convert.ToString(reader["Endereco"]) : String.Empty;
                    aluno.Telefone = (reader["Telefone"] != DBNull.Value) ? Convert.ToString(reader["Telefone"]) : String.Empty;
                    listAlunos.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.ObterAlunosPorProjeto(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listAlunos;
        }

        public static List<Aluno> ObterAlunosPorProjetoSemRegistroFinanceiro(string projeto)
        {
            List<Aluno> listAlunos = new List<Aluno>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", projeto));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT Aluno.* FROM Aluno");
                query.Append(" INNER JOIN Matricula ON Aluno.NumeroPece = Matricula.IdAluno");
                query.Append(" WHERE IdProjeto = @codigoProjeto");
                query.Append(" AND Matricula.idMatricula NOT IN ( SELECT idMatricula FROM Financeiro )");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.NumeroPece = (reader["NumeroPece"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroPece"]) : 0;
                    aluno.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    aluno.Endereco = (reader["Endereco"] != DBNull.Value) ? Convert.ToString(reader["Endereco"]) : String.Empty;
                    aluno.Telefone = (reader["Telefone"] != DBNull.Value) ? Convert.ToString(reader["Telefone"]) : String.Empty;
                    listAlunos.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.ObterAlunosPorProjeto(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listAlunos;
        }

        public static Aluno ObterAlunosPorNumeroPece(int numeroPece)
        {
            Aluno aluno = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@numeroPece", numeroPece));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Aluno");
                query.Append(" WHERE NumeroPece = @numeroPece");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                if (reader.Read())
                {
                    aluno = new Aluno();
                    aluno.NumeroPece = (reader["NumeroPece"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroPece"]) : 0;
                    aluno.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    aluno.Endereco = (reader["Endereco"] != DBNull.Value) ? Convert.ToString(reader["Endereco"]) : String.Empty;
                    aluno.Telefone = (reader["Telefone"] != DBNull.Value) ? Convert.ToString(reader["Telefone"]) : String.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.ObterAlunosPorNumeroPece(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return aluno;
        }

        public static AlunoProjeto ObterRelacionamentoAlunoProjeto(int codigoAluno, string codigoProjeto)
        {
            AlunoProjeto alunoProjeto = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", codigoProjeto));
                parameters.Add(new SqlParameter("@codigoAluno", codigoAluno));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Matricula");
                query.Append(" WHERE IdAluno = @codigoAluno");
                query.Append(" AND IdProjeto = @codigoProjeto");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                if (reader.Read())
                {
                    alunoProjeto = new AlunoProjeto();
                    alunoProjeto.Id = (reader["idMatricula"] != DBNull.Value) ? Convert.ToInt32(reader["idMatricula"]) : 0;
                    alunoProjeto.Status = (StatusAlunoProjeto) Enum.Parse(typeof(StatusAlunoProjeto), (reader["estado"] != DBNull.Value) ? Convert.ToString(reader["estado"]) : "0");
                    alunoProjeto.Aluno = ObterAlunosPorNumeroPece(codigoAluno);
                    alunoProjeto.Projeto = ProjetoDAO.ObterProjetoPorCodigo(codigoProjeto);
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.ObterAlunosPorProjeto(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return alunoProjeto;
        }

        public bool InserirAluno(Aluno aluno)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@NumeroPece", aluno.NumeroPece));
                parameters.Add(new SqlParameter("@Nome", aluno.Nome));
                parameters.Add(new SqlParameter("@Endereco", aluno.Endereco));
                parameters.Add(new SqlParameter("@Telefone", aluno.Telefone));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Aluno");
                query.Append(" (NumeroPece, Nome, Endereco, Telefone)");
                query.Append(" VALUES (@NumeroPece, @Nome, @Endereco, @Telefone)");
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.InserirAluno(Aluno): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public bool InserirVariasMatriculas(Aluno aluno, List<Projeto> projetosDoAluno)
        {
            int affected = 0;
            try
            {
                foreach (Projeto projeto in projetosDoAluno)
                {
                    if (InserirMatricula(aluno, projeto))
                    {
                        affected++;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.InserirVariasMatriculas(Aluno,Projetos): " + ex.ToString(), ex);
            }
            
            return (affected > 0);
        }

       /// <summary>
       /// deprecated
       /// </summary>
       /// <param name="aluno"></param>
       /// <param name="projeto"></param>
       /// <returns></returns>
        private int InserirMatriculaRetornaAffected(Aluno aluno, Projeto projeto)
        {
            
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {       
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@IdAluno", aluno.NumeroPece));
                    parameters.Add(new SqlParameter("@IdProjeto", projeto.Codigo));
                    db.AbreConexao();

                    StringBuilder query = new StringBuilder("INSERT INTO Matricula");
                    query.Append(" (IdAluno, IdProjeto,Estado)");
                    query.Append(" VALUES (@IdAluno, @IdProjeto,0)");
                    affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
                

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.InserirMatriculaRetornaAffected(Aluno,Projeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected);
        }

        public List<Aluno> ObterTodosAlunos()
        {
            List<Aluno> listAlunos = new List<Aluno>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Aluno");
                
                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.NumeroPece = Int32.Parse(reader["NumeroPece"].ToString());
                    aluno.Nome = reader["Nome"].ToString();
                    aluno.Endereco = reader["Endereco"].ToString();
                    aluno.Telefone = reader["Telefone"].ToString();
                    listAlunos.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.ObterTodosAlunos(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listAlunos;
        }

        public Aluno ObterAlunoPeloNumeroPece(int numeroPece)
        {
            Aluno Aluno = new Aluno();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@NumeroPece", numeroPece));
                db.AbreConexao();

                StringBuilder query = new StringBuilder(" SELECT * FROM Aluno ");
                query.Append(" WHERE NumeroPece = @NumeroPece ");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                if (reader.Read())
                {

                    Aluno.NumeroPece = Int32.Parse(reader["NumeroPece"].ToString());
                    Aluno.Nome = reader["Nome"].ToString();
                    Aluno.Endereco = reader["Endereco"].ToString();
                    Aluno.Telefone = reader["Telefone"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.ObterTodosAlunoPeloNumeroPece(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return Aluno;
        }

        public bool AtualizarAluno(Aluno aluno)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Nome", aluno.Nome));
                parameters.Add(new SqlParameter("@Telefone", aluno.Telefone));
                parameters.Add(new SqlParameter("@Endereco", aluno.Endereco));
                parameters.Add(new SqlParameter("@NumeroPece", aluno.NumeroPece));
                
                
                db.AbreConexao();

                StringBuilder query = new StringBuilder("UPDATE Aluno");
                query.Append(" set Nome = @Nome, Telefone=@Telefone, Endereco=@Endereco");
                query.Append(" WHERE (NumeroPece=@NumeroPece)");
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOUAluno.AtualizarAluno(Aluno): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public bool ExcluirMatricula(Aluno aluno, Projeto projeto)
        {

            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@IdAluno", aluno.NumeroPece));
                parameters.Add(new SqlParameter("@IdProjeto", projeto.Codigo));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("DELETE FROM Matricula");
                query.Append(" WHERE IdAluno = @IdAluno AND IdProjeto = @IdProjeto ");
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);


            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.ExcluirMatricula(Aluno,Projeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public bool InserirMatricula(Aluno aluno, Projeto projeto)
        {

            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@IdAluno", aluno.NumeroPece));
                parameters.Add(new SqlParameter("@IdProjeto", projeto.Codigo));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Matricula");
                query.Append(" (IdAluno, IdProjeto,Estado)");
                query.Append(" VALUES (@IdAluno, @IdProjeto,0)");
                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);


            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.InserirMatricula(Aluno,Projeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected>0);
        }

        public bool AlunoPossuiMatricula(int NumeroPece)
        {
            int count = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@IdAluno", NumeroPece));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("Select Count(*) from Matricula");
                query.Append(" WHERE IdAluno = @IdAluno ");
                count = Convert.ToInt32(db.ExecuteScalar(query.ToString(), parameters));


            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.InserirMatricula(Aluno,Projeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (count > 0);
        }

        public bool RemoverAluno(int numeropece)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@NumeroPece", numeropece));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("DELETE FROM Aluno");
                query.Append(" WHERE NumeroPece=@NumeroPece ");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.RemoverAluno(NumeroPece): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }



        public List<Aluno> BuscaAlunoPeloNumeroPeceENome(string NumeroPece, string Nome)
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            List<Aluno> listaalunos = new List<Aluno>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (!NumeroPece.Equals(""))
                {
                    parameters.Add(new SqlParameter("@IdAluno", "%" + NumeroPece + "%"));
                }
                if (!Nome.Equals(""))
                {
                    parameters.Add(new SqlParameter("@Nome", "%" + Nome + "%"));
                }

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Aluno ");
                query.Append(" WHERE ");
                if (!NumeroPece.Equals(""))
                {
                    query.Append(" IdAluno LIKE @IdAluno ");
                }
                if (!NumeroPece.Equals("") && !Nome.Equals(""))
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
                    Aluno aluno = new Aluno();
                    aluno.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["Nome"]) : String.Empty;
                    aluno.NumeroPece = (reader["IdAluno"] != DBNull.Value) ? Convert.ToInt32(reader["IdAluno"]) : 0;
                    aluno.Telefone = (reader["Telefone"] != DBNull.Value) ? Convert.ToString(reader["Telefone"]) : String.Empty;
                    aluno.Endereco = (reader["Endereco"] != DBNull.Value) ? Convert.ToString(reader["Endereco"]) : String.Empty;
                    listaalunos.Add(aluno);

                }
                return listaalunos;
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.BuscaAlunoPeloNumeroPeceENome(NumeroPece,Nome): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
        }
    }
}
