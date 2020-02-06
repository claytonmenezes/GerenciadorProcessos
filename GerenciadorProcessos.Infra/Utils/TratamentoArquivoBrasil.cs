using Ionic.Zip;
using System;
using System.Data;
using System.Data.Odbc;
using System.Net;

namespace GerenciadorProcessos.Infra.Utils
{
    public class TratamentoArquivoBrasil
    {
        public void Download()
        {
            WebClient wc = new WebClient();
            var address = new Uri("http://sigmine.dnpm.gov.br/sirgas2000/Brasil.zip");
            wc.DownloadFile(address, @"C:\Extração\Brasil.zip");
        }
        public void ExtrairZip()
        {
            using (ZipFile zip = new ZipFile(@"C:\Extração\Brasil.zip"))
            {
                foreach (var item in zip.Entries)
                {
                    if (item.FileName.Equals("BRASIL.dbf"))
                    {
                        item.Extract(@"C:\Extração");
                    }
                }
            }
        }
        public DataTable DbfToTable()
        {
            DataTable table = new DataTable();
            OdbcConnection conexao = new OdbcConnection();
            conexao.ConnectionString = @"Driver={Microsoft dBase Driver (*.dbf)};SourceType=DBF;SourceDB=C:\Extração\BRASIL.dbf;Exclusive=No; Collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=NO;";
            conexao.Open();
            OdbcCommand comando = conexao.CreateCommand();
            comando.CommandText = @"SELECT * FROM C:\Extração\BRASIL.dbf";
            table.Load(comando.ExecuteReader());
            conexao.Close();

            return table;
        }
    }
}
