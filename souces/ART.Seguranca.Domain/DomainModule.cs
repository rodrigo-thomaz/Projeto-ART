namespace ART.Seguranca.Domain
{
    using ART.Seguranca.Domain.Interfaces;
    using ART.Seguranca.Domain.Services;

    using Autofac;

    public class DomainModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthDomain>().As<IAuthDomain>();
        }

        #endregion Methods
    }
}