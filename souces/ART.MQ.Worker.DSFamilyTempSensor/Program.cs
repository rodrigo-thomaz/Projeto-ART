using ART.Data.Domain;
using ART.Data.Repository;
using Autofac;
using MassTransit;
using Topshelf;
using Topshelf.Autofac;

namespace ART.MQ.Worker.DSFamilyTempSensor
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ARTDbContext>().InstancePerLifetimeScope();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<DSFamilyTempSensorModule>();

            IContainer container = builder.Build();

            HostFactory.Run(c =>
            {
                c.SetServiceName("ART.MQ.Worker.DSFamilyTempSensor");
                c.SetDisplayName("ART MQ Worker DSFamilyTempSensor");
                c.SetDescription("A ART MQ Worker DSFamilyTempSensor.");

                c.UseAutofacContainer(container);

                c.Service<WorkerService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });
            });
        }
    }
}
