using ART.Data.Repository;
using ART.MQ.Common.QueueNames;
using Autofac;
using Automatonymous;
using MassTransit;
using MassTransit.EntityFrameworkIntegration;
using MassTransit.EntityFrameworkIntegration.Saga;
using MassTransit.Saga;
using System;
using System.Configuration;

namespace ART.MQ.Worker.DSFamilyTempSensor
{
    public class DSFamilyTempSensorModule : Module
    {
        DSFamilyTempSensorStateMachine _machine = new DSFamilyTempSensorStateMachine();
        //ISagaRepository<DSFamilyTempSensorState> _repository = new EntityFrameworkSagaRepository<DSFamilyTempSensorState>(new SagaDbContextFactory(() => new ARTDbContext()));
        ISagaRepository<DSFamilyTempSensorState> _repository = new InMemorySagaRepository<DSFamilyTempSensorState>();

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorkerService>().SingleInstance();            

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
                        //e.Consumer<DSFamilyTempSensorSetResolutionConsumer>(context);
                        e.StateMachineSaga(_machine, _repository);
                    });

                });

                return busControl;
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            //builder.RegisterType<DSFamilyTempSensorStateMachine>();
            
            //builder.RegisterType<DSFamilyTempSensorSetResolutionConsumer>();
        }
    }
}
