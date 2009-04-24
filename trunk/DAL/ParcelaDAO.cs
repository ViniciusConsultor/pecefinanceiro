using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Vsf.Common.Database;
using Vsf.Modelo;

namespace Vsf.DAL
{
    public class ParcelaDAO
    {
        public static bool InserirParcela(Vsf.Modelo.Parcela parcela, int idRegistro)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@numeroParcela", parcela.NumeroParcela));
                parameters.Add(new SqlParameter("@dataVencimento", parcela.DtVencimento));
                parameters.Add(new SqlParameter("@valorPagar", parcela.ValorParcela));
                parameters.Add(new SqlParameter("@idFinanceiro", idRegistro));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO Parcelas");
                query.Append(" (NumeroParcela, DataVencimento, ValorPagar, ValorPago, Observacao, IdFinanceiro, Pago)");
                query.Append(" VALUES (@numeroParcela, @dataVencimento, @valorPagar, 0.0, '', @idFinanceiro, 0)");
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

        public static List<Parcela> ObterParcelasPorRegistro(int idRegistro)
        {
            List<Parcela> listParcela = new List<Parcela>();
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idFinanceiro", idRegistro));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("SELECT * FROM Parcelas ");
                query.Append(" WHERE IdFinanceiro = @idFinanceiro");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                while (reader.Read())
                {
                    Parcela parcela = new Parcela();
                    parcela.ValorParcela = (reader["ValorPagar"] != DBNull.Value)? Convert.ToDouble(reader["ValorPagar"]) : 0.0;
                    parcela.ValorPago = (reader["NumeroParcela"] != DBNull.Value)? Convert.ToDouble(reader["ValorPago"]) : 0.0;
                    parcela.ObservacaoPagamento = (reader["Observacao"] != DBNull.Value)? Convert.ToString(reader["Observacao"]) : String.Empty;
                    parcela.NumeroParcela = (reader["NumeroParcela"] != DBNull.Value)? Convert.ToInt32(reader["NumeroParcela"]) : 0;
                    parcela.DtVencimento = (reader["DataVencimento"] != DBNull.Value)? Convert.ToDateTime(reader["DataVencimento"]) : DateTime.MinValue;
                    parcela.Pago = (reader["Pago"] != DBNull.Value) ? Convert.ToBoolean(reader["Pago"]) : false;
                    listParcela.Add(parcela);
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
            return listParcela;
        }

        public static bool EditarParcela(Parcela parcela, int idRegistro)
        {
            int affected = 0;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@numeroParcela", parcela.NumeroParcela));
                parameters.Add(new SqlParameter("@dataVencimento", parcela.DtVencimento));
                parameters.Add(new SqlParameter("@valorPagar", parcela.ValorParcela));
                parameters.Add(new SqlParameter("@idFinanceiro", idRegistro));
                db.AbreConexao();

                StringBuilder query = new StringBuilder("UPDATE Parcelas SET");
                query.Append(" DataVencimento = @dataVencimento,");
                query.Append(" ValorPagar = @valorPagar");
                query.Append(" WHERE IdFinanceiro = @idFinanceiro");
                query.Append(" AND NumeroParcela = @numeroParcela");

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
    }
}
