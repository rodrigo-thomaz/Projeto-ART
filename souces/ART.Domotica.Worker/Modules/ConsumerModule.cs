namespace ART.Domotica.Worker.Modules
{
    using ART.Domotica.Worker.Consumers;
    using ART.Infra.CrossCutting.Logging;
    using Autofac;
    using Autofac.Extras.DynamicProxy;

    public class ConsumerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationConsumer>()
                .As<IApplicationConsumer>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallDebugLogger))
                .SingleInstance();

            //builder.RegisterType<ApplicationConsumer>().SingleInstance();
            builder.RegisterType<ApplicationUserConsumer>().SingleInstance();
            builder.RegisterType<DSFamilyTempSensorConsumer>().SingleInstance();            
            builder.RegisterType<HardwaresInApplicationConsumer>().SingleInstance();
            builder.RegisterType<TemperatureScaleConsumer>().SingleInstance();
            builder.RegisterType<ThermometerDeviceConsumer>().SingleInstance();

            //var asm = Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(asm)
            //    .Where(x => x.Name.EndsWith("Consumer"))
            //    .SingleInstance()
            //    .EnableClassInterceptors()
            //    .InterceptedBy(typeof(CallDebugLogger));
        }

        #endregion Methods
    }
}