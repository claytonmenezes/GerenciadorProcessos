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
            IJobDetail job = JobBuilder.Create<JobCargaBrasil>().Build();
            IJobDetail jobServerOnline = JobBuilder.Create<JobServerOnline>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithCalendarIntervalSchedule()
                //.WithSchedule(CronScheduleBuilder.CronSchedule("0 00 21 ? * * *"))
                .Build();
            scheduler.ScheduleJob(job, trigger).Wait();

            ITrigger triggerServerOnline = TriggerBuilder.Create().WithCalendarIntervalSchedule()
                .WithSchedule(CronScheduleBuilder.CronSchedule("0 0/10 * 1/1 * ? *"))
                .Build();
            scheduler.ScheduleJob(jobServerOnline, triggerServerOnline).Wait();
        }
    }
}
