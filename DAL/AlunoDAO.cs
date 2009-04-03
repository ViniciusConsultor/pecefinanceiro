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
                if (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.NumeroPece = (reader["NumeroPece"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroPece"]) : 0;
                    aluno.Nome = (reader["Nome"] != DBNull.Value) ? Convert.ToString(reader["NumeroPece"]) : String.Empty;
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
    }
}
