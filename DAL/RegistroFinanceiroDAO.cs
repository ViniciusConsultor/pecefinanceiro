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
        public static int IncluirRegistroFinanceiro(Vsf.Modelo.RegistroFinanceiro registroFinanceiro, Vsf.Modelo.AlunoProjeto alunoProjeto)
        {
            int newId = 0;
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

                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Financeiro");
                query.Append(" (idMatricula, NumeroParcelas, PrecoReajustado, Observacoes, DiaPagamento, PrimeiraParcela)");
                query.Append(" VALUES (@idMatricula, @numeroParcelas, @precoReajustado, @observacoes, @diaPagamento, @primeiraParcela)");
                query.Append(" SELECT SCOPE_IDENTITY()");

                newId = Convert.ToInt32(db.ExecuteScalar(query.ToString(), parameters));
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

            return newId;
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
                    registro.IdRegistro = (reader["IdFinanceiro"] != DBNull.Value) ? Convert.ToInt32(reader["IdFinanceiro"]) : 0;
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

        public static RegistroFinanceiro ObterRegistroPorMatricula(int idMatricula)
        {
            RegistroFinanceiro registroFinanceiro = new RegistroFinanceiro();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idMatricula", idMatricula));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Financeiro");
                query.Append(" INNER JOIN Matricula ON Financeiro.idMatricula = Matricula.idMatricula");
                query.Append(" WHERE Financeiro.idMatricula = @idMatricula");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    registroFinanceiro.IdRegistro = (reader["IdFinanceiro"] != DBNull.Value) ? Convert.ToInt32(reader["IdFinanceiro"]) : 0;
                    registroFinanceiro.AlunoProjeto = AlunoDAO.ObterRelacionamentoAlunoProjeto(Convert.ToInt32(reader["IdAluno"]), Convert.ToString(reader["IdProjeto"]));
                    registroFinanceiro.DataVencimentoPrimeiraParcela = (reader["PrimeiraParcela"] != DBNull.Value) ? Convert.ToDateTime(reader["PrimeiraParcela"]) : DateTime.MinValue;
                    registroFinanceiro.DiaPagamento = (reader["DiaPagamento"] != DBNull.Value) ? Convert.ToDateTime(reader["DiaPagamento"]).Day : 0;
                    registroFinanceiro.NumeroParcelas = (reader["NumeroParcelas"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroParcelas"]) : 0;
                    registroFinanceiro.Observacoes = (reader["Observacoes"] != DBNull.Value) ? Convert.ToString(reader["Observacoes"]) : String.Empty;
                    registroFinanceiro.PrecoReajustado = (reader["PrecoReajustado"] != DBNull.Value) ? Convert.ToDouble(reader["PrecoReajustado"]) : 0.0;
                    registroFinanceiro.Status = (StatusAlunoProjeto)Enum.Parse(typeof(StatusAlunoProjeto), (reader["estado"] != DBNull.Value) ? Convert.ToString(reader["estado"]) : "0");
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.ObterRegistroPorId(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return registroFinanceiro;
        }

        public static RegistroFinanceiro ObterRegistroPorId(int idRegistro)
        {
            RegistroFinanceiro registroFinanceiro = new RegistroFinanceiro();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idRegistro", idRegistro));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Financeiro");
                query.Append(" INNER JOIN Matricula ON Financeiro.idMatricula = Matricula.idMatricula");
                query.Append(" WHERE Financeiro.idFinanceiro = @idRegistro");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    registroFinanceiro.IdRegistro = (reader["IdFinanceiro"] != DBNull.Value) ? Convert.ToInt32(reader["IdFinanceiro"]) : 0;
                    registroFinanceiro.AlunoProjeto = AlunoDAO.ObterRelacionamentoAlunoProjeto(Convert.ToInt32(reader["IdAluno"]), Convert.ToString(reader["IdProjeto"]));
                    registroFinanceiro.DataVencimentoPrimeiraParcela = (reader["PrimeiraParcela"] != DBNull.Value) ? Convert.ToDateTime(reader["PrimeiraParcela"]) : DateTime.MinValue;
                    registroFinanceiro.DiaPagamento = (reader["DiaPagamento"] != DBNull.Value) ? Convert.ToDateTime(reader["DiaPagamento"]).Day : 0;
                    registroFinanceiro.NumeroParcelas = (reader["NumeroParcelas"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroParcelas"]) : 0;
                    registroFinanceiro.Observacoes = (reader["Observacoes"] != DBNull.Value) ? Convert.ToString(reader["Observacoes"]) : String.Empty;
                    registroFinanceiro.PrecoReajustado = (reader["PrecoReajustado"] != DBNull.Value) ? Convert.ToDouble(reader["PrecoReajustado"]) : 0.0;
                    registroFinanceiro.Status = (StatusAlunoProjeto)Enum.Parse(typeof(StatusAlunoProjeto), (reader["estado"] != DBNull.Value) ? Convert.ToString(reader["estado"]) : "0");
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOAluno.ObterRegistroPorId(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return registroFinanceiro;
        }


        public static bool AtualizarRegistroFinanceiro(RegistroFinanceiro registroFinanceiro)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idMatricula", registroFinanceiro.AlunoProjeto.Id));
                parameters.Add(new SqlParameter("@numeroParcelas", registroFinanceiro.NumeroParcelas));
                parameters.Add(new SqlParameter("@precoReajustado", registroFinanceiro.PrecoReajustado));
                parameters.Add(new SqlParameter("@observacoes", registroFinanceiro.Observacoes));
                parameters.Add(new SqlParameter("@diaPagamento", registroFinanceiro.DiaPagamento));
                parameters.Add(new SqlParameter("@primeiraParcela", registroFinanceiro.DataVencimentoPrimeiraParcela));
                parameters.Add(new SqlParameter("@status", registroFinanceiro.Status));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("UPDATE Financeiro SET");
                query.Append(" NumeroParcelas = @numeroParcelas,");
                query.Append(" PrecoReajustado = @precoReajustado,");
                query.Append(" Observacoes = @observacoes,");
                query.Append(" DiaPagamento = @diaPagamento,");
                query.Append(" PrimeiraParcela = @primeiraParcela");
                query.Append(" WHERE idMatricula = @idMatricula");

                affected = db.ExecuteTextNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("DAOProjeto.AtualizarRegistroFinanceiro(codigoProjeto): " + ex.ToString(), ex);
            }
            finally
            {
                db.FechaConexao();
            }

            Logger.Registrar(1, "RegistroFinanceiro inserido para ProjetoAluno número " + registroFinanceiro.AlunoProjeto.Id + ".");

            return (affected > 0);
        }



        public static List<RegistroFinanceiro> BuscarRegistrosPorAlunoEProjeto(string aluno, string projeto)
        {
            List<RegistroFinanceiro> listRegistros = new List<RegistroFinanceiro>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@aluno", aluno));
                parameters.Add(new SqlParameter("@projeto", projeto));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Financeiro");
                query.Append(" INNER JOIN Matricula ON Financeiro.idMatricula = Matricula.idMatricula");
                query.Append(" LEFT JOIN Aluno ON Matricula.IdAluno = Aluno.NumeroPece");
                query.Append(" LEFT JOIN Projeto ON Matricula.IdProjeto = Projeto.CodigoProjeto");
                if (!projeto.Equals(String.Empty))
                {
                    query.Append(" WHERE Projeto.Nome LIKE @projeto");
                    if (!aluno.Equals(String.Empty))
                        query.Append("      AND Aluno.Nome LIKE @aluno");
                } else if(!aluno.Equals(String.Empty))
                    query.Append(" WHERE Aluno.Nome LIKE @aluno");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    RegistroFinanceiro registro = new RegistroFinanceiro();
                    registro.IdRegistro = (reader["IdFinanceiro"] != DBNull.Value) ? Convert.ToInt32(reader["IdFinanceiro"]) : 0;
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


