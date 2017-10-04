namespace ART.Seguranca.Repository
{

    using Autofac;

    public class RepositoryModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<AuthRepository>().As<IAuthRepository>();
        }

        #endregion Methods
    }
}