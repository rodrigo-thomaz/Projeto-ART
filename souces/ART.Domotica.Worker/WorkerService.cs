namespace ART.Domotica.Worker
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Job;
    using ART.Infra.CrossCutting.Setting;

    using Quartz;

    using RabbitMQ.Client;

    public class WorkerService
    {
        #region Fields

        private readonly IConnection _connection;
        private readonly IScheduler _scheduler;
        private readonly ISettingManager _settingManager;

        #endregion Fields

        #region Constructors

        public WorkerService(IConnection connection, IScheduler scheduler, ISettingManager settingManager)
        {
            _connection = connection;
            _scheduler = scheduler;
            _settingManager = settingManager;
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
            Task.Run(async () =>
            {
                //try
                //{
                //    var exists = await _settingManager.Exist("Teste");
                //    await _settingManager.Insert("Teste");
                //    var result1 = await _settingManager.GetValue<int>("Teste");
                //    await _settingManager.SetValue("Teste", 99);
                //    var result2 = await _settingManager.GetValue<int>("Teste");
                //    await _settingManager.Delete("Teste");
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}

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
            });
        }

        #endregion Methods
    }
}