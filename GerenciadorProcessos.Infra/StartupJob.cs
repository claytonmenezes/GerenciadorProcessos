using GerenciadorProcessos.Infra.Repositorios.Geral;
using GerenciadorProcessos.Infra.Utils;
using Quartz;
using Quartz.Impl;
using System.Linq;

namespace GerenciadorProcessos.Infra
{
    public class StartupJob
    {
        public static void StartScheduler()
        {
            var repoParSistema = new RepositorioParametroSistema();
            var parametro = repoParSistema.Listar().FirstOrDefault();

            if (parametro.AtualizaArquivoBrasil)
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = factory.GetScheduler().Result;
                scheduler.Start().Wait();
                ScheduleJobs(scheduler);
            }
        }
        private static void ScheduleJobs(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<JobCargaBrasil>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithCalendarIntervalSchedule()
                .WithSchedule(CronScheduleBuilder.CronSchedule("0 30 18 ? * * *"))
                .Build();
            scheduler.ScheduleJob(job, trigger).Wait();
        }
    }
}
