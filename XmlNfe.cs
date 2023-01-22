using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProcessarXmlConsole
{
    public class XmlNfe
    {
        private string chaveNF;
        private string local;
        private string emissor;

        public XmlNfe()
        {
            this.chaveNF = null;
            this.local = null;
            this.emissor = null;
        }

        public void SetChaveNF(string pChave)
        {
            this.chaveNF = pChave;
        }

        [Obsolete]
        public string AdicionarXml(OracleConnection connection)
        {

            ConexaoOracle con1 = new ConexaoOracle();

            //Descobrir o CNPJ da empresa
            string sqlQuery = "select cgc from empresa";
            OracleDataReader reader = con1.ConsultarDados(sqlQuery, connection);
            if (reader.Read())
            {
                this.emissor = reader.GetString(0);
            }
            reader.Close();

            //Descobrir o local que está salvo o XML
            sqlQuery = "select a.local_xml_autzdo from nfepack.interf_nfe a where a.a_id = '" + this.chaveNF + "' and a.c_cnpj = '" + this.emissor + "'";
            reader = con1.ConsultarDados(sqlQuery, connection);
            if (reader.Read() == true)
            {
                this.local = reader.GetString(0);
            }
            reader.Close();

            //Inserir os dados com uma das colunas do tipo CLOB
            sqlQuery = "insert into xml_nfe values ('" + this.chaveNF + "', :blobparam)";

            //Converter o conteúdo do arquivo referenciado na variável sLocal para um array chamado byteArrayparam
            FileStream fs = new FileStream(this.local, FileMode.Open, FileAccess.Read);

            Byte[] byteArrayparam = new Byte[fs.Length];
            fs.Read(byteArrayparam, 0, byteArrayparam.Length);
            fs.Close();

            //Salvar o conteúdo no DB Oracle
            string sMsg = con1.InserirBlob(sqlQuery, byteArrayparam, connection);

            return sMsg;
        }

        public void EnviarEmail(List<string> list)
        {
            dynamic obj1 = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient
            {
                Host = obj1.server_smtp.Value,
                EnableSsl = false,
                Credentials = new System.Net.NetworkCredential(obj1.email_from.Value, obj1.password_email_from.Value)
            };

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage
            {
                Sender = new System.Net.Mail.MailAddress(obj1.email_from.Value, obj1.email_from.Value),
                From = new System.Net.Mail.MailAddress(obj1.email_from.Value, obj1.email_from.Value)
            };
            mail.To.Add(new System.Net.Mail.MailAddress(obj1.email_to.Value, obj1.email_to.Value));
            mail.Subject = "Erro ao gravar XML no software ProcessarXmlNfe";

            string sMsg = "Não foi possível gravar o XML da(s) nota(s) abaixo. Favor verificar! <br>";
            sMsg += "<br>";

            foreach (string s in list)
            {
                sMsg += s + "<br>";
            }

            mail.Body = sMsg;
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;            
            client.Send(mail);
        }

        public void EnviaEmail()
        {

            SmtpClient client = new SmtpClient();
            client.Host = "mail.cristina.com.br";
            MailAddress de = new MailAddress("ti@cristina.com.br");
            MailAddress para = new MailAddress("william.ti@cristina.com.br");
            MailMessage mensagem = new MailMessage(de, para);
            mensagem.IsBodyHtml = true;
            mensagem.Subject = "Integração VTEX";
            mensagem.Body = "";
            
            try
            {
                client.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return;
        }
    }

}
