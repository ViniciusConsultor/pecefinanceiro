using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Common.Database;
using System.Data.SqlClient;
using Vsf.Modelo;

namespace Vsf.DAL
{
    public class RegistroFinanceiroDAO
    {
        public static bool IncluirRegistroFinanceiro(Vsf.Modelo.RegistroFinanceiro registroFinanceiro, Vsf.Modelo.AlunoProjeto alunoProjeto)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idProjetoAluno", alunoProjeto.Id));
                parameters.Add(new SqlParameter("@numeroParcelas", registroFinanceiro.NumeroParcelas));
                parameters.Add(new SqlParameter("@precoReajustado", registroFinanceiro.PrecoReajustado));
                parameters.Add(new SqlParameter("@observacoes", registroFinanceiro.Observacoes));
                parameters.Add(new SqlParameter("@diaPagamento", registroFinanceiro.DiaPagamento));
                parameters.Add(new SqlParameter("@primeiraParcela", registroFinanceiro.DataVencimentoPrimeiraParcela));
                parameters.Add(new SqlParameter("@status", registroFinanceiro.Status));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Financeiro");
                query.Append(" (idProjetoAluno, NumeroParcelas, PrecoReajustado, Observacoes, DiaPagamento, PrimeiraParcela, Estado)");
                query.Append(" VALUES (@idProjetoAluno, @numeroParcelas, @precoReajustado, @observacoes, @diaPagamento, @primeiraParcela, @status)");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("DAOProjeto.ObterProjetoPorCodigo(codigoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return (affected > 0);
        }

        public static List<RegistroFinanceiro> ObterTodosRegistros()
        {
            List<RegistroFinanceiro> listRegistros = new List<RegistroFinanceiro>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();                

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Financeiro");
                query.Append(" INNER JOIN ProjetoAluno ON Financeiro.idProjetoAluno = ProjetoAluno.idProjetoAluno");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    RegistroFinanceiro registro = new RegistroFinanceiro();
                    registro.AlunoProjeto = AlunoDAO.ObterRelacionamentoAlunoProjeto(Convert.ToInt32(reader["IdAluno"]), Convert.ToString(reader["IdProjeto"]));
                    registro.DataVencimentoPrimeiraParcela = (reader["PrimeiraParcela"] != DBNull.Value) ? Convert.ToDateTime(reader["PrimeiraParcela"]) : DateTime.MinValue;
                    registro.DiaPagamento = (reader["DiaPagamento"] != DBNull.Value) ? Convert.ToInt32(reader["DiaPagamento"]) : 0;
                    registro.NumeroParcelas = (reader["NumeroParcelas"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroParcelas"]) : 0;
                    registro.Observacoes = (reader["Observacoes"] != DBNull.Value) ? Convert.ToString(reader["Observacoes"]) : String.Empty;
                    registro.PrecoReajustado = (reader["PrecoReajustado"] != DBNull.Value) ? Convert.ToDouble(reader["PrecoReajustado"]) : 0.0;
                    registro.Status = (StatusRegistroFinanceiro)Enum.Parse(typeof(StatusRegistroFinanceiro), (reader["status"] != DBNull.Value) ? Convert.ToString(reader["status"]) : "0");
                    
                    listRegistros.Add(registro);
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
            return listRegistros;
        }
    }
}

