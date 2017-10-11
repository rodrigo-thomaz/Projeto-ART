namespace ART.Domotica.WebApi.Modules
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using System.Reflection;

    public class ControllerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers("Controller", Assembly.GetExecutingAssembly());
                //.EnableInterfaceInterceptors()
                //.InterceptedBy(typeof(CallDebugLogger)); 
        }

        #endregion Methods
    }
}