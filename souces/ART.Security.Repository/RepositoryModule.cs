namespace ART.Security.Repository
{
    using ART.Security.Repository.Interfaces;
    using ART.Security.Repository.Repositories;

    using Autofac;

    public class RepositoryModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthRepository>().As<IAuthRepository>();
        }

        #endregion Methods
    }
}