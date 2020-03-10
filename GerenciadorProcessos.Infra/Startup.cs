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
            //IJobDetail job = JobBuilder.Create<JobCargaBrasil>().Build();
            //IJobDetail jobServerOnline = JobBuilder.Create<JobServerOnline>().Build();

            //ITrigger trigger = TriggerBuilder.Create().WithCalendarIntervalSchedule()
            //    .WithSchedule(CronScheduleBuilder.CronSchedule("0 30 15 ? * * *"))
            //    .Build();
            //scheduler.ScheduleJob(job, trigger).Wait();

            //ITrigger triggerServerOnline = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
            //    .Build();
            //scheduler.ScheduleJob(jobServerOnline, triggerServerOnline).Wait();
        }
    }
}
