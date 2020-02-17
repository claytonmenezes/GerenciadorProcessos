using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GerenciadorProcessos.Infra.Utils
{
    public class Jobs : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var tratamentoArquivo = new TratamentoArquivoBrasil();
            var bancoDados = new BancoDados();

            return Task.Run(() => {
                if (Directory.Exists(@"C:\Extração")) Directory.Delete(@"C:\Extração", true);
                Directory.CreateDirectory(@"C:\Extração");

                tratamentoArquivo.Download();
                tratamentoArquivo.ExtrairZip();
                var importacaoId = bancoDados.ExecutarComando("select max(ImportacaoId) from ImpBrasil");
                importacaoId = importacaoId != DBNull.Value ? importacaoId : 0;
                var dataTable = tratamentoArquivo.DbfToTable((int)importacaoId);
                bancoDados.inserirBanco(dataTable);

                Directory.Delete(@"C:\Extração", true);
            });
        }
    }
}
