using Quartz;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GerenciadorProcessos.Infra.Utils
{
    public class JobServerOnline : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {
                WebClient wc = new WebClient();
                var address = new Uri("http://localhost:59420/api/ServerOnline/Ping");
                var t = wc.DownloadData(address);
            });
        }
    }
}
