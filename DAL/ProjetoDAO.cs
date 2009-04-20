﻿using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using System.Data.SqlClient;
using Vsf.Common.Database;
using Vsf.Common;

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
    }
}
