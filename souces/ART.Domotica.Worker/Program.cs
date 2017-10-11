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

    using Topshelf;
    using Topshelf.Autofac;

    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
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

            HostFactory.Run(c =>
            {
                c.SetServiceName("ART.Domotica.Worker");
                c.SetDisplayName("ART MQ Worker");
                c.SetDescription("A ART MQ Worker.");

                c.UseAutofacContainer(container);

                c.Service<WorkerService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });
            });
        }

        #endregion Methods
    }
}