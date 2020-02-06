using Quartz;
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
                var dataTable = tratamentoArquivo.DbfToTable();
                bancoDados.inserirBanco(dataTable);

                Directory.Delete(@"C:\Extração", true);
            });
        }
    }
}
