namespace ART.Domotica.Worker.Modules
{
    using System.Reflection;

    using ART.Infra.CrossCutting.Logging;

    using Autofac;
    using Autofac.Extras.DynamicProxy;

    public class ConsumerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ApplicationConsumer>()
            //    .As<IApplicationConsumer>()
            //    .SingleInstance()
            //    //.AsImplementedInterfaces()
            //    .EnableInterfaceInterceptors()
            //    .InterceptedBy(typeof(CallDebugLogger));

            //builder.RegisterType<ApplicationConsumer>().As<IApplicationConsumer>().SingleInstance();
            //builder.RegisterType<ApplicationUserConsumer>().As<IApplicationUserConsumer>().SingleInstance();
            //builder.RegisterType<DSFamilyTempSensorConsumer>().As<IDSFamilyTempSensorConsumer>().SingleInstance();
            //builder.RegisterType<HardwaresInApplicationConsumer>().As<IHardwaresInApplicationConsumer>().SingleInstance();
            //builder.RegisterType<TemperatureScaleConsumer>().As<ITemperatureScaleConsumer>().SingleInstance();
            //builder.RegisterType<ThermometerDeviceConsumer>().As<IThermometerDeviceConsumer>().SingleInstance();

            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(x => x.Name.EndsWith("Consumer"))
                .SingleInstance()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallDebugLogger));
        }

        #endregion Methods
    }
}