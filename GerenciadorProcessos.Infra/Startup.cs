using GerenciadorProcessos.Infra.Utils;
using Quartz;
using Quartz.Impl;

namespace GerenciadorProcessos.Infra
{
    public class Startup
    {
        public static void StartScheduler()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();
            ScheduleJobs(scheduler);
        }
        private static void ScheduleJobs(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<Jobs>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithCalendarIntervalSchedule()
                .WithSchedule(CronScheduleBuilder.CronSchedule("0 13 14 ? * * *"))
                .Build();
            scheduler.ScheduleJob(job, trigger).Wait();
        }
    }
}
