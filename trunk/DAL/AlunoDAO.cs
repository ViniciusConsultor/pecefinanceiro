using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using Vsf.Common.Database;
using System.Data.SqlClient;

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
                query.Append(" INNER JOIN ProjetoAluno ON Aluno.NumeroPece = ProjetoAluno.IdAluno");
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
                throw new ApplicationException("DAOAluno.ObterAlunosPorProjeto(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listAlunos;
        }

        public List<Aluno> ObterAlunosPorProjetoSemRegistroFinanceiro(Projeto projeto)
        {
            List<Aluno> listAlunos = new List<Aluno>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@codigoProjeto", projeto.Codigo));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT Aluno.* FROM Aluno");
                query.Append(" INNER JOIN ProjetoAluno ON Aluno.NumeroPece = ProjetoAluno.IdAluno");
                query.Append(" WHERE IdProjeto = @codigoProjeto");
                query.Append(" AND ProjetoAluno.idProjetoAluno NOT IN ( SELECT idProjetoAluno FROM Financeiro )");

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

                StringBuilder query = new StringBuilder("SELECT * FROM ProjetoAluno");
                query.Append(" WHERE IdAluno = @codigoAluno");
                query.Append(" AND IdProjeto = @codigoProjeto");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                if (reader.Read())
                {
                    alunoProjeto = new AlunoProjeto();
                    alunoProjeto.Id = (reader["idProjetoAluno"] != DBNull.Value) ? Convert.ToInt32(reader["idProjetoAluno"]) : 0;
                    alunoProjeto.Status = (StatusAlunoProjeto) Enum.Parse(typeof(StatusAlunoProjeto), (reader["status"] != DBNull.Value) ? Convert.ToString(reader["status"]) : "0");
                    alunoProjeto.Aluno = ObterAlunosPorNumeroPece(codigoAluno);
                    alunoProjeto.Projeto = ProjetoDAO.ObterProjetoPorCodigo(codigoProjeto);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOAluno.ObterAlunosPorProjeto(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return alunoProjeto;
        }
    }
}
