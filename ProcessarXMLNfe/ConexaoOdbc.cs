using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessarXMLNfe
{
    class ConexaoOdbc
    {
        public OdbcConnection conectarBanco(System.Data.Odbc.OdbcConnection connection)
        {
            connection.ConnectionString = "Dsn=dbteste;uid=vestis01;pwd=vestis01";
            connection.Open();
            return connection;
        }

        public void desconectarBanco(OdbcConnection connection)
        {
            connection.Close();
        }

        public OdbcDataReader consultarDados(string consultaSql, OdbcConnection connection)
        {            
            OdbcCommand command = new OdbcCommand(consultaSql, connection);
            OdbcDataReader reader = command.ExecuteReader();
            return reader;
        }

        public Boolean inserirDados(string insertSql, OdbcConnection connection)
        {
            try
            {
                OdbcCommand command = new OdbcCommand(insertSql);
                command.Connection = connection;
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
