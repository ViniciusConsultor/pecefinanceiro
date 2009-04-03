using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using System.Data.SqlClient;
using Vsf.Common.Database;

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
                if (reader.Read())
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
                throw new ApplicationException("DAOProjeto.ObterTodosProjetos(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listProjeto;
        }
    }
}
