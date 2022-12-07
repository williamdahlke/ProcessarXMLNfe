﻿using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessarXMLNfe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bt_processar_Click(object sender, EventArgs e)
        {
            if (txt_chave.Text == "")
            {
                MessageBox.Show("É necessário informar a chave da NF para realizar o processamento.");
                return;
            }

            try
            {
                ConexaoOracle con = new ConexaoOracle();
                OracleConnection connection = new OracleConnection();
                con.conectarBanco(connection);
                
                //Descobrir o CNPJ da empresa
                string sCgc = "";
                string sqlQuery = "select cgc from empresa";                               
                OracleDataReader readerEmpresa = con.consultarDados(sqlQuery, connection);
                if (readerEmpresa.Read() == true)
                {
                    sCgc = readerEmpresa.GetString(0);
                }
                else
                {
                    MessageBox.Show("Não foi possível encontrar os dados da empresa.");
                    return;
                }
                readerEmpresa.Close();

                //Descobrir o local que está salvo o XML
                string sLocal = "";
                sqlQuery = "select a.local_xml_autzdo from nfepack.interf_nfe a where a.a_id = '" + txt_chave.Text + "' and a.c_cnpj = '" + sCgc + "'";
                OracleDataReader readerNota = con.consultarDados(sqlQuery, connection);
                if (readerNota.Read() == true)
                {
                    sLocal = readerNota.GetString(0);
                } else {
                    MessageBox.Show("Não foi possível processar a chave: " + txt_chave.Text);
                    return;
                }
                readerNota.Close();
                con.desconectarBanco(connection);

                //Inserir os dados com uma das colunas do tipo CLOB
                sqlQuery = "insert into xml_nfe values ('" + txt_chave.Text + "', :blobparam)";
                con.conectarBanco(connection);               

                //Converter o conteúdo do arquivo referenciado na variável sLocal para um array chamado byteArrayparam
                FileStream fs = new FileStream(sLocal, FileMode.Open, FileAccess.Read);
                Byte[] byteArrayparam = null;
                byteArrayparam = new Byte[fs.Length];
                fs.Read(byteArrayparam, 0, byteArrayparam.Length);
                fs.Close();

                //Salvar o conteúdo no DB Oracle
                Boolean bSalvou = con.inserirBlob(sqlQuery, byteArrayparam, connection);

                if (bSalvou == true)
                {
                    MessageBox.Show("Os dados do XML foram salvos com sucesso.");
                }
                else
                {
                    MessageBox.Show("Os dados do XML não foram salvos. Favor verificar!");
                    return;
                }

                con.desconectarBanco(connection);
                connection.Close();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
