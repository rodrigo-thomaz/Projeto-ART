namespace ART.Domotica.Repository
{
    using ART.Infra.CrossCutting.Logging;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using System.Reflection;

    public class RepositoryModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallDebugLogger));
        }

        #endregion Methods
    }
}