using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProcessarXmlConsole
{
    public class ConexaoOracle
    {
        [Obsolete]
        public OracleConnection ConectarBanco(OracleConnection connection)
        {
            dynamic obj = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));

            string sConnectionString = obj.connection_string_oracle.Value;

            sConnectionString = sConnectionString.Replace("@1", obj.connection_user.Value);
            sConnectionString = sConnectionString.Replace("@2", obj.connection_password.Value);

            connection.ConnectionString = sConnectionString;
            connection.Open();
            return connection;
        }

        [Obsolete]
        public void DesconectarBanco(OracleConnection connection)
        {
            connection.Close();
        }

        [Obsolete]
        public OracleDataReader ConsultarDados(string consultaSql, OracleConnection connection)
        {
            OracleCommand command = new OracleCommand(consultaSql, connection);
            OracleDataReader reader = command.ExecuteReader();
            return reader;
        }

        [Obsolete]
        public string InserirClob(string insertSql, string sXml, OracleConnection connection)
        {
            OracleCommand command = new OracleCommand
            {
                Connection = connection,
                CommandText = insertSql
            };

            OracleTransaction transaction;
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                OracleParameter clobparam = new OracleParameter("clobparam", OracleType.Clob, sXml.Length)
                {
                    Direction = ParameterDirection.Input,
                    Value = sXml
                };
                command.Parameters.Add(clobparam);
                command.ExecuteNonQuery();
                transaction.Commit();

                return null;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return e.Message;
            }
        }

        [Obsolete]
        public string InserirBlob(string insertSql, Byte[] byteArrayparam, OracleConnection connection)
        {

            OracleCommand command = new OracleCommand
            {
                Connection = connection,
                CommandText = insertSql
            };

            OracleTransaction transaction;
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                OracleParameter blobparam = new OracleParameter("blobparam", OracleType.Blob, byteArrayparam.Length)
                {
                    Direction = ParameterDirection.Input,
                    Value = byteArrayparam
                };
                command.Parameters.Add(blobparam);
                command.ExecuteNonQuery();
                transaction.Commit();
                return null;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return e.Message;
            }
        }

    }
}
