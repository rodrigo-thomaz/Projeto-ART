namespace ART.Domotica.Domain
{
    using System.Reflection;

    using ART.Infra.CrossCutting.Logging;

    using Autofac;
    using Autofac.Extras.DynamicProxy;

    public class DomainModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(x => x.Name.EndsWith("Domain"))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallDebugLogger));
        }

        #endregion Methods
    }
}