using ART.MQ.Common.QueueNames;
using ART.MQ.Consumer.Consumers.DSFamilyTempSensorConsumers;
using ART.MQ.Consumer.IDomain;
using ART.MQ.Consumer.IRepositories;
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
            var builder = new ContainerBuilder();

            // register repositories
            builder.RegisterType<IDSFamilyTempSensorRepository>();

            // register domain services
            builder.RegisterType<IDSFamilyTempSensorDomain>();            

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
                        e.Consumer<DSFamilyTempSensorSetResolutionConsumer>();
                    });

                });

                return busControl;
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            var container = builder.Build();

            var bc = container.Resolve<IBusControl>();
            bc.Start();

            Console.ReadKey();

            bc.Stop();
        }

        //static IBusControl ConfigureBus()
        //{
        //    return Bus.Factory.CreateUsingRabbitMq(rabbit =>
        //    {
        //        var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];
        //        var virtualHostName = ConfigurationManager.AppSettings["RabbitMQVirtualHostName"];
        //        var username = ConfigurationManager.AppSettings["RabbitMQUsername"];
        //        var password = ConfigurationManager.AppSettings["RabbitMQPassword"];

        //        rabbit.Host(hostName, virtualHostName, settings =>
        //        {
        //            settings.Username(username);
        //            settings.Password(password);
        //        });
        //    });
        //}
    }
}
