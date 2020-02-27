using Quartz;
using System.Threading.Tasks;

namespace GerenciadorProcessos.Infra.Utils
{
    public class JobServerOnline : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {});
        }
    }
}
