namespace ART.Security.Domain
{
    using ART.Security.Domain.Interfaces;
    using ART.Security.Domain.IProducers;
    using ART.Security.Domain.Producers;
    using ART.Security.Domain.Services;

    using Autofac;

    public class DomainModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthProducer>().As<IAuthProducer>();

            builder.RegisterType<AuthDomain>().As<IAuthDomain>();
        }

        #endregion Methods
    }
}