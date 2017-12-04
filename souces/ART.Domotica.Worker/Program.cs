namespace ART.Domotica.Worker
{
    using System;

    using ART.Domotica.Constant;
    using ART.Domotica.Domain;
    using ART.Domotica.Repository;
    using ART.Domotica.Worker.AutoMapper;
    using ART.Domotica.Worker.AutoMapper.Locale;
    using ART.Domotica.Worker.AutoMapper.SI;
    using ART.Domotica.Worker.Jobs;
    using ART.Domotica.Worker.Modules;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.Scheduler;
    using ART.Infra.CrossCutting.Setting;

    using Autofac;

    using global::AutoMapper;

    using Quartz;
    using Quartz.Spi;

    using Topshelf;
    using Topshelf.Autofac;
    using Topshelf.Quartz;

    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var builder = new ContainerBuilder();

            builder.RegisterType<WorkerService>();

            builder.RegisterType<ARTDbContext>().InstancePerDependency();

            // CrossCutting Modules

            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<SettingModule>();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<MQModule>();
            builder.RegisterModule<ConsumerModule>();
            builder.RegisterModule<JobModule>();

            builder.RegisterModule<SchedulerModule>();

            Mapper.Initialize(x =>
            {
                //SI
                x.AddProfile(new NumericalScalePrefixProfile());
                x.AddProfile(new NumericalScaleProfile());
                x.AddProfile(new NumericalScaleTypeCountryProfile());
                x.AddProfile(new NumericalScaleTypeProfile());
                x.AddProfile(new UnitMeasurementProfile());
                x.AddProfile(new UnitMeasurementScaleProfile());
                x.AddProfile(new UnitMeasurementTypeProfile());

                x.AddProfile(new ApplicationMQProfile());
                x.AddProfile(new ApplicationProfile());
                x.AddProfile(new ApplicationUserProfile());
                x.AddProfile(new ContinentProfile());
                x.AddProfile(new CountryProfile());
                x.AddProfile(new DeviceMQProfile());
                x.AddProfile(new DeviceNTPProfile());
                x.AddProfile(new SensorTempDSFamilyProfile());
                x.AddProfile(new ESPDeviceProfile());
                x.AddProfile(new SensorUnitMeasurementScaleProfile());
                x.AddProfile(new SensorDatasheetProfile());
                x.AddProfile(new SensorProfile());
                x.AddProfile(new SensorTriggerProfile());
                x.AddProfile(new SensorTypeProfile());
                x.AddProfile(new SensorUnitMeasurementDefaultProfile());
                x.AddProfile(new SensorDatasheetUnitMeasurementScaleProfile());
                x.AddProfile(new TimeZoneProfile());
            });

            IContainer container = builder.Build();

            ScheduleJobServiceConfiguratorExtensions.SchedulerFactory = () => container.Resolve<IScheduler>();

            HostFactory.Run(x =>
            {
                x.UseAssemblyInfoForServiceInfo();

                x.UseAutofacContainer(container);

                x.Service<WorkerService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });

                x.SetStartTimeout(TimeSpan.FromSeconds(10));
                x.SetStopTimeout(TimeSpan.FromSeconds(10));

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(3);
                    //r.RunProgram(7, "ping google.com");
                    //r.RestartComputer(5, "message");
                    r.OnCrashOnly();
                    r.SetResetPeriod(2);
                });

                x.OnException((exception) =>
                {
                    Console.WriteLine("Exception thrown - " + exception.Message);
                });

                x.UsingQuartzJobFactory(() => container.Resolve<IJobFactory>());

                x.ScheduleQuartzJobAsService(q =>
                {
                    q.WithJob(() => JobBuilder.Create<UpdatePinJob>().Build());
                    q.AddTrigger(() => TriggerBuilder.Create().WithSimpleSchedule(b =>
                    {
                        var settingManager = container.Resolve<ISettingManager>();
                        var interval = settingManager.GetValue<int>(SettingsConstants.ChangePinIntervalInSecondsSettingsKey);
                        b.WithIntervalInSeconds(interval).RepeatForever();
                    }).Build());
                });

            });
        }

        #endregion Methods
    }
}