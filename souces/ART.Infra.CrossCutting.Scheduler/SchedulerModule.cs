namespace ART.Infra.CrossCutting.Scheduler
{
    using System.Collections.Specialized;

    using Autofac;
    using Autofac.Extras.Quartz;

    public class SchedulerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            var schedulerConfig = new NameValueCollection {
                {"quartz.threadPool.threadCount", "3"},
                {"quartz.threadPool.threadNamePrefix", "SchedulerWorker"},
                {"quartz.scheduler.threadName", "Scheduler"}
            };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });
        }

        #endregion Methods
    }
}