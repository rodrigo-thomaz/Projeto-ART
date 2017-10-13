namespace ART.Domotica.Worker
{
    using ART.Domotica.Job;
    using Quartz;
    using RabbitMQ.Client;

    public class WorkerService
    {
        #region Fields

        private readonly IConnection _connection;
        private readonly IScheduler _scheduler;

        #endregion Fields

        #region Constructors

        public WorkerService(IConnection connection, IScheduler scheduler)
        {
            _connection = connection;
            _scheduler = scheduler;
        }

        #endregion Constructors

        #region Methods

        public bool Start()
        {
            _scheduler.Start();
            
            ConfigureUpdatePinJob();            

            return true;
        }

        public bool Stop()
        {
            _connection.Close(30);
            _scheduler.Shutdown();
            log4net.LogManager.Shutdown();
            return true;
        }
        
        private void ConfigureUpdatePinJob()
        {
            IJobDetail job = JobBuilder.Create<UpdatePinJob>()
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();


            _scheduler.ScheduleJob(job, trigger);
        }

        #endregion Methods
    }
}