using ART.MQ.Consumer.Modules;
using Autofac;
using MassTransit;
using System;

namespace ART.MQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateContainer();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ARTDbContext>().InstancePerLifetimeScope();
            
            builder.RegisterModule<DSFamilyTempSensorModule>();
            builder.RegisterModule<BusModule>();

            IContainer container = builder.Build();

            var busControl = container.Resolve<IBusControl>();

            busControl.Start();

            Console.ReadKey();

            busControl.Stop();

            return container;
        }

    }
}
