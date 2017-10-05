namespace ART.Infra.CrossCutting.MQ
{
    using System.Configuration;

    using Autofac;

    using RabbitMQ.Client;

    public class MQModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            // Register your Bus
            builder.Register(c =>
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

                IConnection conn = factory.CreateConnection();

                return conn;
            })
                .As<IConnection>()
                .SingleInstance();
        }

        #endregion Methods
    }
}