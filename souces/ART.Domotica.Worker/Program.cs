namespace ART.Domotica.Worker
{
    using ART.Domotica.Domain;
    using ART.Domotica.Domain.AutoMapper;
    using ART.Domotica.Repository;
    using ART.Domotica.Worker.Modules;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ;

    using Autofac;

    using global::AutoMapper;
    using System;
    using Topshelf;
    using Topshelf.Autofac;

    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var builder = new ContainerBuilder();
            
            builder.RegisterType<WorkerService>();

            builder.RegisterType<ARTDbContext>().InstancePerDependency();

            builder.RegisterModule<LoggingModule>();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<MQModule>();
            builder.RegisterModule<ConsumerModule>();

            Mapper.Initialize(x =>
            {
                x.AddProfile(new ApplicationProfile());
                x.AddProfile(new ApplicationUserProfile());
                x.AddProfile(new DSFamilyTempSensorProfile());
                x.AddProfile(new HardwaresInApplicationProfile());
                x.AddProfile(new TemperatureScaleProfile());
                x.AddProfile(new ThermometerDeviceProfile());
            });

            IContainer container = builder.Build();

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
            });
        }

        #endregion Methods
    }
}