using ART.MQ.Common.QueueNames;
using ART.MQ.Consumer.Consumers.DSFamilyTempSensorConsumers;
using ART.MQ.Consumer.Domain;
using ART.MQ.Consumer.IDomain;
using ART.MQ.Consumer.IRepositories;
using ART.MQ.Consumer.Repositories;
using Autofac;
using MassTransit;
using System;
using System.Configuration;
using System.Reflection;

namespace ART.MQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = null;

            var builder = new ContainerBuilder();

            // register repositories
            builder.RegisterType<DSFamilyTempSensorRepository>().As<IDSFamilyTempSensorRepository>();

            // register domain services
            builder.RegisterType<DSFamilyTempSensorDomain>().As<IDSFamilyTempSensorDomain>();

            // just register all the consumers
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());

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
        }
    }
}
