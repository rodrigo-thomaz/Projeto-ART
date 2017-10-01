using ART.MQ.Worker.Modules;
using Autofac;
using MassTransit;
using Topshelf;
using Topshelf.Autofac;

namespace ART.MQ.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();            

            builder.RegisterType<ARTDbContext>().InstancePerLifetimeScope();

            builder.RegisterModule<BusModule>();
            builder.RegisterModule<DSFamilyTempSensorModule>();            

            IContainer container = builder.Build();

            HostFactory.Run(c =>
            {
                c.SetServiceName("ART.MQ.Worker");
                c.SetDisplayName("ART MQ Consumer");
                c.SetDescription("A ART MQ Consumer.");
                
                c.UseAutofacContainer(container);

                c.Service<BusControlService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });              
            });            
        }
    }

    
}
