using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessarXMLNfe
{
    class ConexaoOracle
    {
        public OracleConnection conectarBanco(OracleConnection connection)
        {
            connection.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.17)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME = cristi)));User Id=vestis01;Password=vestis01;";
            connection.Open();
            return connection;
        }

        public void desconectarBanco(OracleConnection connection)
        {
            connection.Close();
        }

        public OracleDataReader consultarDados(string consultaSql, OracleConnection connection)
        {
            OracleCommand command = new OracleCommand(consultaSql, connection);           
            OracleDataReader reader = command.ExecuteReader();
            return reader;
        }

        public Boolean inserirDados(string insertSql, OracleConnection connection)
        {
            try
            {
                OracleCommand command = new OracleCommand(insertSql);
                command.Connection = connection;                
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }        
        }

        public Boolean inserirClob(string insertSql, string sXml, OracleConnection connection)
        {
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = connection;
                command.CommandText = insertSql;

                OracleParameter clobparam = new OracleParameter("clobparam", OracleType.Clob, sXml.Length);
                clobparam.Direction = ParameterDirection.Input;
                clobparam.Value = sXml;
                command.Parameters.Add(clobparam);                
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean inserirBlob(string insertSql, Byte[] byteArrayparam, OracleConnection connection)
        {
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = connection;
                command.CommandText = insertSql;                

                OracleParameter blobparam = new OracleParameter("blobparam", OracleType.Blob, byteArrayparam.Length);
                blobparam.Direction = ParameterDirection.Input;
                blobparam.Value = byteArrayparam;
                command.Parameters.Add(blobparam);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
