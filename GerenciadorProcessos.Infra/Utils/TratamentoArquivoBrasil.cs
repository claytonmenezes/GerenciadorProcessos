using Ionic.Zip;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace GerenciadorProcessos.Infra.Utils
{
    public class TratamentoArquivoBrasil
    {
        public SqlConnection conexao = new SqlConnection();

        public void ExtrairZip()
        {
            using (ZipFile zip = new ZipFile(@"C:\Extração\Brasil.zip"))
            {
                try
                {
                    zip.ExtractAll(@"C:\Extração");
                }
                catch
                {
                    throw;
                }
            }
        }
        public void Download()
        {
            WebClient wc = new WebClient();
            var address = new Uri("http://sigmine.dnpm.gov.br/sirgas2000/Brasil.zip");
            wc.DownloadFile(address, @"C:\Extração\Brasil.zip");
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
        public void CriaAmbiente()
        {
            Directory.CreateDirectory(@"C:\Extração");
        }
        public void PadronizaAmbiente()
        {
            if (Directory.Exists(@"C:\Extração"))
            {
                Directory.Delete(@"C:\Extração", true);
            }
        }
        public void inserirBanco(DataTable dataTable)
        {
            Conectar();
            try
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(conexao))
                {
                    bulk.DestinationTableName = "teste";
                    bulk.WriteToServer(dataTable);
                }
            }
            finally
            {
                Desconectar();
            }
        }
        public void ExecutarComando (string stringComando)
        {
            var comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = stringComando;
            comando.ExecuteNonQuery();
        }
        private void Conectar()
        {
            if (conexao.State == ConnectionState.Closed)
            {
                conexao.ConnectionString = "Data Source=.; Initial Catalog=dbGerenciadorProcessos; Integrated Security=True; MultipleActiveResultSets=True";
                conexao.Open();
            }
        }
        private void Desconectar()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
