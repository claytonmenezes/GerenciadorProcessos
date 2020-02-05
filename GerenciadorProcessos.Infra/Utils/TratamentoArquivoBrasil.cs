using Ionic.Zip;
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

        public MemoryStream ExtrairZip(MemoryStream streamRar)
        {
            using (ZipFile zip = ZipFile.Read(streamRar))
            {
                zip["Brasil.dbf"].Extract(streamRar);
            }
            streamRar.Seek(0, SeekOrigin.Begin);
            return streamRar;
        }
        public MemoryStream Download()
        {
            WebClient wc = new WebClient();
            using (MemoryStream stream = new MemoryStream(wc.DownloadData("http://sigmine.dnpm.gov.br/sirgas2000/Brasil.zip")))
            {
                //byte[] bytes = new byte[stream.Length];
                //stream.Read(bytes, 0, (int)stream.Length);
                using (ZipFile zip = ZipFile.Read(stream))
                {
                    zip["BRASIL.dbf"].Extract(stream);
                }
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }
        public DataTable StreamToTable(MemoryStream stream)
        {
            DataTable table = new DataTable();
            OdbcConnection conexao = new OdbcConnection();
            conexao.ConnectionString = @"Driver={Microsoft dBase Driver (*.dbf)};SourceType=DBF;SourceDB=" + stream + ";Exclusive=No; Collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=NO;";
            conexao.Open();
            OdbcCommand comando = conexao.CreateCommand();
            comando.CommandText = @"SELECT * FROM " + stream;
            table.Load(comando.ExecuteReader());
            conexao.Close();

            return table;
        }
        public void CriaAmbiente()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Extração");
        }
        public void PadronizaAmbiente()
        {
            Directory.Delete(Directory.GetCurrentDirectory() + @"\Extração", true);
            File.Delete(Directory.GetCurrentDirectory() + @"\Brasil.zip");
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
