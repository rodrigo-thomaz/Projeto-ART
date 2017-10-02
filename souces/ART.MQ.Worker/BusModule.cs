using Autofac;
using RabbitMQ.Client;
using System.Configuration;

namespace ART.MQ.Worker
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorkerService>();           

            builder.Register(context =>
            {
                var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];
                var virtualHost = ConfigurationManager.AppSettings["RabbitMQVirtualHost"];
                var username = ConfigurationManager.AppSettings["RabbitMQUsername"];
                var password = ConfigurationManager.AppSettings["RabbitMQPassword"];

                var factory = new ConnectionFactory();

                factory.UserName = username;
                factory.Password = password;
                factory.VirtualHost = virtualHost;
                factory.HostName = hostName;

                IConnection connection = factory.CreateConnection();

                return connection;
            })
                .As<IConnection>()
                .SingleInstance();
        }
    }
}
