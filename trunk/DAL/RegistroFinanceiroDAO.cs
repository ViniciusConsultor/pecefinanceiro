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
    public class RegistroFinanceiroDAO
    {
        public static bool IncluirRegistroFinanceiro(Vsf.Modelo.RegistroFinanceiro registroFinanceiro, Vsf.Modelo.AlunoProjeto alunoProjeto)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idMatricula", alunoProjeto.Id));
                parameters.Add(new SqlParameter("@numeroParcelas", registroFinanceiro.NumeroParcelas));
                parameters.Add(new SqlParameter("@precoReajustado", registroFinanceiro.PrecoReajustado));
                parameters.Add(new SqlParameter("@observacoes", registroFinanceiro.Observacoes));
                parameters.Add(new SqlParameter("@diaPagamento", registroFinanceiro.DiaPagamento));
                parameters.Add(new SqlParameter("@primeiraParcela", registroFinanceiro.DataVencimentoPrimeiraParcela));
                parameters.Add(new SqlParameter("@status", registroFinanceiro.Status));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Financeiro");
                query.Append(" (idMatricula, NumeroParcelas, PrecoReajustado, Observacoes, DiaPagamento, PrimeiraParcela, Estado)");
                query.Append(" VALUES (@idMatricula, @numeroParcelas, @precoReajustado, @observacoes, @diaPagamento, @primeiraParcela, @status)");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
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

            Logger.Registrar(1, "RegistroFinanceiro inserido para ProjetoAluno número " + alunoProjeto.Id + ".");

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
                query.Append(" INNER JOIN Matricula ON Financeiro.idMatricula = Matricula.idMatricula");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    RegistroFinanceiro registro = new RegistroFinanceiro();
                    registro.AlunoProjeto = AlunoDAO.ObterRelacionamentoAlunoProjeto(Convert.ToInt32(reader["IdAluno"]), Convert.ToString(reader["IdProjeto"]));
                    registro.DataVencimentoPrimeiraParcela = (reader["PrimeiraParcela"] != DBNull.Value) ? Convert.ToDateTime(reader["PrimeiraParcela"]) : DateTime.MinValue;
                    registro.DiaPagamento = (reader["DiaPagamento"] != DBNull.Value) ? Convert.ToDateTime(reader["DiaPagamento"]).Day : 0;
                    registro.NumeroParcelas = (reader["NumeroParcelas"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroParcelas"]) : 0;
                    registro.Observacoes = (reader["Observacoes"] != DBNull.Value) ? Convert.ToString(reader["Observacoes"]) : String.Empty;
                    registro.PrecoReajustado = (reader["PrecoReajustado"] != DBNull.Value) ? Convert.ToDouble(reader["PrecoReajustado"]) : 0.0;
                    registro.Status = (StatusAlunoProjeto)Enum.Parse(typeof(StatusAlunoProjeto), (reader["estado"] != DBNull.Value) ? Convert.ToString(reader["estado"]) : "0");
                    
                    listRegistros.Add(registro);
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
            return listRegistros;
        }
    }
}

