using ART.Consumer.IDomain;
using ART.Consumer.IRepositories;
using Autofac;
using MassTransit;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ART.Consumer
{
    public class UpdateCustomerAddressConsumer : IConsumer<object>
    {
        public async Task Consume(ConsumeContext<object> context)
        {
            //do stuff
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // register repositories
            builder.RegisterType<IDSFamilyTempSensorRepository>();

            // register domain services
            builder.RegisterType<IDSFamilyTempSensorDomain>();            

            // register a specific consumer
            builder.RegisterType<UpdateCustomerAddressConsumer> ();

            // just register all the consumers
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());

            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://file-server/"), h =>
                    {
                        h.Username("test");
                        h.Password("test");
                    });

                    cfg.ReceiveEndpoint("ARTPUBTEMP", ec =>
                    {
                        ec.LoadFrom(context);
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
    }
}
