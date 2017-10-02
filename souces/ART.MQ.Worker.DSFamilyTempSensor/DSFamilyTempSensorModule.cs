using ART.MQ.Common.QueueNames;
using Autofac;
using MassTransit;
using System;
using System.Configuration;

namespace ART.MQ.Worker.DSFamilyTempSensor
{
    public class DSFamilyTempSensorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorkerService>();

            builder.RegisterType<DSFamilyTempSensorSetResolutionConsumer>();

            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
                {
                    var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];
                    var username = ConfigurationManager.AppSettings["RabbitMQUsername"];
                    var password = ConfigurationManager.AppSettings["RabbitMQPassword"];

                    var host = rabbit.Host(new Uri(hostName), settings =>
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
        }
    }
}
