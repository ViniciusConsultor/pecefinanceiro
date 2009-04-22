using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using Vsf.Common.Database;
using System.Data.SqlClient;
using Vsf.Common;

namespace Vsf.DAL
{
    public class RelatorioDAO
    {
        public static Relatorio ObterRelatorioInadimplentes()
        {
            Relatorio relatorio = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {            
                db.AbreConexao();
                List<SqlParameter> parameters = new List<SqlParameter>(); 
                StringBuilder query = new StringBuilder("select SUM(ValorPagar) as total, COUNT(alu.Nome) as alunos, ");
                query.Append("AVG(datediff(DD ,DataVencimento,GETDATE())) as 'media atrasos', ");
                query.Append("MAX(datediff(DD ,DataVencimento,GETDATE())) as 'maior atraso' ");
                query.Append("FROM Parcelas as parc ");
                query.Append("INNER JOIN Financeiro as finc ");
                query.Append("on finc.IdFinanceiro = parc.IdFinanceiro ");
                query.Append("INNER JOIN Matricula as mat ");
                query.Append("ON mat.IdMatricula = finc.IdMatricula ");
                query.Append("INNER JOIN Aluno as alu " );
                query.Append("ON alu.NumeroPece = mat.IdAluno ");
                query.Append("where Pago = 0 and parc.DataVencimento < GETDATE() and mat.Estado = 0 ");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(),parameters);
                if (reader.Read())
                {
                    relatorio = new Relatorio();
                    relatorio.ValorTotal = (reader["total"] != DBNull.Value) ? Convert.ToDecimal(reader["total"]) : 0 ;
                    relatorio.NumAlunos = Convert.ToInt32(reader["alunos"]);
                    relatorio.MediaDiasAtrasados = (reader["media atrasos"] != DBNull.Value) ? Convert.ToDecimal(reader["media atrasos"]) : 0;
                    relatorio.MaiorAtraso = (reader["maior atraso"] != DBNull.Value) ? Convert.ToInt32(reader["maior atraso"]) : 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("RelatorioAluno.ObterRelatorioInadimplentes(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return relatorio;
        }
        public static Relatorio ObterRelatorioArrecadacaoMes(int mes, int ano, enumTipoRelatorio tipo)
        {
            Relatorio relatorio = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                db.AbreConexao();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@mes", mes));
                parameters.Add(new SqlParameter("@ano", ano));
                
                StringBuilder query = new StringBuilder("select COUNT(alu.Nome) as alunos, ");
                query.Append("COUNT(proj.Nome) as projetos");
                switch (tipo)
                {
                    case enumTipoRelatorio.Efetivo:
                        query.Append(", SUM(ValorPago) as total, ");
                        query.Append("sum(ValorPago - ValorPagar) as 'Rendimento Juros', ");
                        query.Append("sum(ValorPagar) as 'Arrecadacao Prevista' ");
                        break;
                    case enumTipoRelatorio.Previsto:
                        query.Append(" ,SUM(ValorPagar) as total ");
                        break;
                    case enumTipoRelatorio.Devido:
                        query.Append(" ,SUM(ValorPagar) as total ");
                        break;
                }
                query.Append("FROM Parcelas as parc ");
                query.Append("INNER JOIN Financeiro as finc ");
                query.Append("on finc.IdFinanceiro = parc.IdFinanceiro ");
                query.Append("INNER JOIN Matricula as mat ");
                query.Append("ON mat.IdMatricula = finc.IdMatricula ");
                query.Append("INNER JOIN Aluno as alu ");
                query.Append("ON alu.NumeroPece = mat.IdAluno ");
                query.Append("INNER JOIN projeto as proj ");
	            query.Append("ON proj.CodigoProjeto = mat.IdProjeto ");
                query.Append("where month(parc.DataVencimento) = @mes ");
                query.Append("and year(parc.DataVencimento) = @ano ");
                switch (tipo)
                {
                    case enumTipoRelatorio.Efetivo:
                        query.Append(" and Pago = 1 ");
                        break;
                    case enumTipoRelatorio.Previsto:
                        query.Append("");
                        break;
                    case enumTipoRelatorio.Devido:
                        query.Append(" and parc.DataVencimento < GETDATE()");
                        query.Append(" and Pago = 0 ");
                        break;
                }
                 query.Append("and mat.Estado = 0 ");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                if (reader.Read())
                {
                    relatorio = new Relatorio();
                    relatorio.ValorTotal = (reader["total"] != DBNull.Value) ? Convert.ToDecimal(reader["total"]) : 0;
                    relatorio.NumAlunos = (reader["alunos"] != DBNull.Value) ? Convert.ToInt32(reader["alunos"]) : 0;
                    relatorio.NumProjetos = (reader["projetos"] != DBNull.Value) ? Convert.ToInt32(reader["projetos"]) : 0;
                    if (tipo == enumTipoRelatorio.Efetivo)
                    {
                        relatorio.ValorJuros = (reader["Rendimento Juros"] != DBNull.Value) ? Convert.ToDecimal(reader["Rendimento Juros"]) : 0;
                        relatorio.ValorPrevisto = (reader["Arrecadacao Prevista"] != DBNull.Value) ? Convert.ToDecimal(reader["Arrecadacao Prevista"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("RelatorioAluno.ObterRelatorioArrecadacaoMes(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return relatorio;
        }
       
        public static List<AlunoParcela> ObterAlunosInadimplentes()
        {
            List<AlunoParcela>  listaAlunoParcela = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                db.AbreConexao();
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder query = new StringBuilder("select alu.Nome as aluno, alu.NumeroPece, ");
                query.Append("parc.DataVencimento, parc.ValorPagar ");
                query.Append("FROM Parcelas as parc ");
                query.Append("INNER JOIN Financeiro as finc ");
                query.Append("on finc.IdFinanceiro = parc.IdFinanceiro ");
                query.Append("INNER JOIN Matricula as mat ");
                query.Append("ON mat.IdMatricula = finc.IdMatricula ");
                query.Append("INNER JOIN Aluno as alu ");
                query.Append("ON alu.NumeroPece = mat.IdAluno ");
                query.Append("where Pago = 0 and parc.DataVencimento < GETDATE() and mat.Estado = 0 ");
                query.Append("order by DataVencimento");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                listaAlunoParcela = new List<AlunoParcela>();
                while (reader.Read())
                {
                    AlunoParcela alunoParcela = new AlunoParcela();
                    alunoParcela.Nome = (reader["aluno"] != DBNull.Value) ? Convert.ToString(reader["aluno"]) : string.Empty;
                    alunoParcela.NumeroPece = (reader["NumeroPece"] != DBNull.Value) ? Convert.ToInt32(reader["NumeroPece"]) : 0;
                    alunoParcela.ParcelaVencida = (reader["DataVencimento"] != DBNull.Value) ? Convert.ToDateTime(reader["DataVencimento"]) : DateTime.MinValue;
                    alunoParcela.ValorParcela = (reader["ValorPagar"] != DBNull.Value) ? Convert.ToDecimal(reader["ValorPagar"]) : 0;
                    listaAlunoParcela.Add(alunoParcela);
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("RelatorioAluno.ObterAlunosInadimplentes(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listaAlunoParcela;
        }
        public static List<Projeto> ObterArrecadacaoMes(int mes, int ano, enumTipoRelatorio tipo)
        {
            List<Projeto> listaProjeto = null;
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                db.AbreConexao();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@mes", mes));
                parameters.Add(new SqlParameter("@ano", ano));
                StringBuilder query = new StringBuilder("select proj.Nome as Projeto, proj.CodigoProjeto, ");
                switch (tipo)
                {
                    case enumTipoRelatorio.Efetivo:
                        query.Append("SUM(parc.ValorPago) 'Valor' ");
                        break;
                    case enumTipoRelatorio.Previsto:
                        query.Append("SUM(parc.ValorPagar) 'Valor' ");
                        break;
                    case enumTipoRelatorio.Devido:
                        query.Append("SUM(parc.ValorPagar) 'Valor' ");
                        break;
                }
                query.Append("FROM Parcelas as parc ");
                query.Append("INNER JOIN Financeiro as finc ");
                query.Append("on finc.IdFinanceiro = parc.IdFinanceiro ");
                query.Append("INNER JOIN Matricula as mat ");
                query.Append("ON mat.IdMatricula = finc.IdMatricula ");
                query.Append("INNER JOIN projeto as proj ");
                query.Append("ON proj.CodigoProjeto = mat.IdProjeto ");
                query.Append("where month(parc.DataVencimento) = @mes ");
                query.Append("and year(parc.DataVencimento) = @ano ");
                switch (tipo)
                {
                    case enumTipoRelatorio.Efetivo:
                        query.Append("and Pago = 1  ");
                        break;
                    case enumTipoRelatorio.Previsto:
                        query.Append(""); //
                        break;
                    case enumTipoRelatorio.Devido:
                        query.Append("and parc.DataVencimento < GETDATE() ");
                        query.Append("and Pago = 0 ");
                        break;
                }
                query.Append("and mat.Estado = 0 ");
                query.Append("GROUP BY proj.Nome, proj.CodigoProjeto ");

                SqlDataReader reader = db.ExecuteTextReader(query.ToString(), parameters);
                listaProjeto = new List<Projeto>();
                while (reader.Read())
                {
                    Projeto projeto = new Projeto();
                    projeto.Nome = (reader["Projeto"] != DBNull.Value) ? Convert.ToString(reader["Projeto"]) : string.Empty;
                    projeto.Codigo = (reader["CodigoProjeto"] != DBNull.Value) ? Convert.ToString(reader["CodigoProjeto"]) : string.Empty;
                    projeto.Valor = (reader["Valor"] != DBNull.Value) ? Convert.ToDouble(reader["Valor"]) : 0;
                    listaProjeto.Add(projeto);
                }
            }
            catch (Exception ex)
            {
                Logger.Registrar(0, "Exceção em (DAO) " + ex.Source + " - " + ex.ToString() + " : " + ex.Message + "\n\n StackTrace: " + ex.StackTrace);
                throw new ApplicationException("RelatorioAluno.ObterArrecadacaoMes(): " + ex, ex);
            }
            finally
            {
                db.FechaConexao();
            }
            return listaProjeto;

        }



    }
}
