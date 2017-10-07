namespace ART.Security.Producer
{
    using ART.Security.Producer.Interfaces;
    using ART.Security.Producer.Services;

    using Autofac;

    public class ProducerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthProducer>().As<IAuthProducer>();
        }

        #endregion Methods
    }
}