using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GerenciadorProcessos.Infra.Utils
{
    public class BancoDados
    {
        private SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["GerenciadorProcessos"].ConnectionString);

        public void inserirBanco(DataTable dataTable, int importacaoId)
        {
            Conectar();
            try
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(conexao))
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (column.ColumnName.Equals("Expr1000"))
                        {
                            bulk.ColumnMappings.Add(new SqlBulkCopyColumnMapping() { SourceColumn = column.ColumnName, DestinationColumn = "ImportacaoId" });
                        }
                        else
                        {
                            bulk.ColumnMappings.Add(new SqlBulkCopyColumnMapping() { SourceColumn = column.ColumnName, DestinationColumn = column.ColumnName });
                        }
                    }
                    bulk.DestinationTableName = "ImpBrasil";
                    bulk.WriteToServer(dataTable);
                    ExecutarComando("exec prInsereFasesNovas " + importacaoId);
                    ExecutarComando("exec prInsereTiposEventosNovos " + importacaoId);
                    if (importacaoId == 1)
                    {
                        ExecutarComando("exec prInsereProcessosPrimeiraImportacao " + importacaoId);
                    }
                    else
                    {
                        ExecutarComando("exec prInsereProcessosNovos " + importacaoId);
                    }
                }
            }
            finally
            {
                Desconectar();
            }
        }
        public object ExecutarComando(string stringComando)
        {
            Conectar();
            try
            {
                var comando = new SqlCommand();
                comando.CommandTimeout = 0;
                comando.Connection = conexao;
                comando.CommandText = stringComando;
                return comando.ExecuteScalar();
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
