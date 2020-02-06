using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GerenciadorProcessos.Infra.Utils
{
    public class BancoDados
    {
        private SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["dbGerenciadorProcessos"].ConnectionString);

        public void inserirBanco(DataTable dataTable)
        {
            Conectar();
            try
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(conexao))
                {
                    ExecutarComando(@"
                        CREATE TABLE #temp(
	                        PROCESSO varchar(max),
	                        ID varchar(max),
	                        NUMERO varchar(max),
	                        ANO varchar(max),
	                        AREA_HA varchar(max),
	                        FASE varchar(max),
	                        ULT_EVENTO varchar(max),
	                        NOME varchar(max),
	                        SUBS varchar(max),
	                        USO varchar(max),
	                        UF varchar(max)
                        )
                    ");
                    bulk.DestinationTableName = "#temp";
                    bulk.WriteToServer(dataTable);
                    ExecutarComando("");
                    ExecutarComando("drop table #temp");
                }
            }
            finally
            {
                Desconectar();
            }
        }
        public void ExecutarComando(string stringComando)
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
