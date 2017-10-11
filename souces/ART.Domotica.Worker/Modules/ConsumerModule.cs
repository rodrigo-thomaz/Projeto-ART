namespace ART.Domotica.Worker.Modules
{

    using ART.Infra.CrossCutting.Logging;

    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using ART.Domotica.Worker.Consumers;
    using ART.Domotica.Worker.IConsumers;

    public class ConsumerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationConsumer>()
                .As<IApplicationConsumer>()
                .SingleInstance()
                .AutoActivate()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CallDebugLogger));

            //builder.RegisterType<ApplicationConsumer>().As<IApplicationConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<ApplicationUserConsumer>().As<IApplicationUserConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<DSFamilyTempSensorConsumer>().As<IDSFamilyTempSensorConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<HardwaresInApplicationConsumer>().As<IHardwaresInApplicationConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<TemperatureScaleConsumer>().As<ITemperatureScaleConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<ThermometerDeviceConsumer>().As<IThermometerDeviceConsumer>().SingleInstance().AutoActivate();

            //var asm = Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(asm)
            //    .Where(x => x.Name.EndsWith("Consumer"))
            //    .SingleInstance()
            //    .AutoActivate()
            //    .AsImplementedInterfaces()
            //    .EnableInterfaceInterceptors()
            //    .InterceptedBy(typeof(CallDebugLogger));
        }

        #endregion Methods
    }
}