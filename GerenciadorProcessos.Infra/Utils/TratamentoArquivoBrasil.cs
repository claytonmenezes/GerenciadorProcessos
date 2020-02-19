using Ionic.Zip;
using System;
using System.Data;
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
        public DataTable DbfToTable(int importacaoId)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ImportacaoId");
            using (var dbfReader = new DbfDataReader.DbfDataReader(@"C:\Extração\BRASIL.dbf"))
            {
                foreach (var column in dbfReader.DbfTable.Columns)
                {
                    table.Columns.Add(column.Name);
                }
                dbfReader.DbfTable.SkipToFirstRecord(dbfReader.DbfTable.BinaryReader);
                while (dbfReader.DbfRecord.Read(dbfReader.DbfTable.BinaryReader))
                {
                    object[] dataRow = new object[table.Columns.Count];
                    dataRow.SetValue(importacaoId, 0);
                    int count = 1;
                    foreach (var itemRow in dbfReader.DbfRecord.Values)
                    {
                        dataRow.SetValue(itemRow.ToString(), count);
                        count++;
                    }
                    table.Rows.Add(dataRow);
                }
            }
            return table;
        }
        private string MetoodoConversao (string textoErrado)
        {
            string textoCerto = "";
            return textoCerto;
        }
    }
}
