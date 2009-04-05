using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Common.Database;
using System.Data.SqlClient;

namespace Vsf.Common
{
    public static class Logger
    {
        public static void Registrar(int idUsuario, string Mensagem)
        {
            VsfDatabase db = new VsfDatabase(Properties.Settings.Default.StringConexao);
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                parameters.Add(new SqlParameter("@texto", Mensagem));
                parameters.Add(new SqlParameter("@data", DateTime.Now));

                db.AbreConexao();

                StringBuilder query = new StringBuilder("INSERT INTO LogFinanceiro");
                query.Append(" (idUsuario, Texto, dtOcorrencia)");
                query.Append(" VALUES (@idUsuario, @texto, @data)");

                db.ExecuteTextNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
                //Do nothing;
            }
            finally
            {
                db.FechaConexao();
            }
        }
    }
}
