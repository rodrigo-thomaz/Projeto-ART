using ART.MQ.Common.QueueNames;
using ART.MQ.Consumer.Consumers.DSFamilyTempSensorConsumers;
using ART.MQ.Consumer.Modules;
using Autofac;
using MassTransit;
using System;
using System.Configuration;

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
            IContainer container = null;

            var builder = new ContainerBuilder();

            builder.RegisterType<ARTDbContext>().InstancePerLifetimeScope();
            
            builder.RegisterModule<DSFamilyTempSensorModule>();
            
            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
                {
                    var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];
                    var virtualHostName = ConfigurationManager.AppSettings["RabbitMQVirtualHostName"];
                    var username = ConfigurationManager.AppSettings["RabbitMQUsername"];
                    var password = ConfigurationManager.AppSettings["RabbitMQPassword"];

                    var host = rabbit.Host(hostName, virtualHostName, settings =>
                    {
                        settings.Username(username);
                        settings.Password(password);
                    });

                    rabbit.ReceiveEndpoint(host, DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetResolutionQueue, e =>
                    {
                        e.Consumer<DSFamilyTempSensorSetResolutionConsumer>(context);
                    });

                });

                return busControl;
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            container = builder.Build();

            var bc = container.Resolve<IBusControl>();
            bc.Start();

            Console.ReadKey();

            bc.Stop();

            return container;
        }

    }
}
