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
                tratamentoArquivo.PadronizaAmbiente();
                tratamentoArquivo.CriaAmbiente();
                tratamentoArquivo.Download();
                tratamentoArquivo.ExtrairZip();
                var dataTable = tratamentoArquivo.DbfToTable();
                tratamentoArquivo.inserirBanco(dataTable);
                tratamentoArquivo.PadronizaAmbiente();
            });
        }
    }
}
