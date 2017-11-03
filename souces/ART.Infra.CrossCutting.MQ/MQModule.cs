namespace ART.Infra.CrossCutting.MQ
{
    using Autofac;

    using RabbitMQ.Client;

    public class MQModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MQSettings>()
                .As<IMQSettings>()
                .SingleInstance();

            // Register your Bus
            builder.Register(c =>
            {
                var settingManager = c.Resolve<IMQSettings>();
                settingManager.Initialize();

                var factory = new ConnectionFactory();

                factory.HostName = settingManager.BrokerHost;
                factory.VirtualHost = settingManager.BrokerVirtualHost;
                factory.UserName = settingManager.BrokerUser;
                factory.Password = settingManager.BrokerPwd;

                IConnection conn = factory.CreateConnection();

                return conn;
            })
                .As<IConnection>()
                .SingleInstance();
        }

        #endregion Methods
    }
}