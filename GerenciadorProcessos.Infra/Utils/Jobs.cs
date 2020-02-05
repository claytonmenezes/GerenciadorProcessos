using Quartz;
using System.Threading.Tasks;

namespace GerenciadorProcessos.Infra.Utils
{
    public class Jobs : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {
                var tratamentoArquivo = new TratamentoArquivoBrasil();
                //tratamentoArquivo.CriaAmbiente();
                var stream = tratamentoArquivo.Download();
                //var streamArquivo = tratamentoArquivo.ExtrairZip(streamRar);
                var dataTable = tratamentoArquivo.StreamToTable(stream);
                //tratamentoArquivo.inserirBanco(dataTable);
                tratamentoArquivo.PadronizaAmbiente();
            });
        }
    }
}
