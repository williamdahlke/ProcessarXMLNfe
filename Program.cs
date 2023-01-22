using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessarXmlConsole
{
    internal class Program
    {
        static void Main()
        {
            XmlNfe xmlNfe = new XmlNfe();

            OracleConnection connection = new OracleConnection();
            ConexaoOracle con = new ConexaoOracle();
            con.ConectarBanco(connection);

            string sQuery = "select a.chave_nfe from v_nf_proc_xml a";
            OracleDataReader reader = con.ConsultarDados(sQuery, connection);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    xmlNfe.SetChaveNF(reader.GetString(0));
                    string sMsg = xmlNfe.AdicionarXml(connection);

                    if (sMsg != null)
                    {
                        string sItem = reader.GetString(0) + " - " + sMsg;
                        list.Add(sItem);
                    }
                }

                if (list.Count > 0)
                {
                    xmlNfe.EnviarEmail(list);
                    //xmlNfe.EnviaEmail();
                }
            }

            con.DesconectarBanco(connection);
            connection.Close();
        }
    }
}
