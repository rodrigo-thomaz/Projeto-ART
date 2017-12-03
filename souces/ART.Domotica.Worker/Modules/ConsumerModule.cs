namespace ART.Domotica.Worker.Modules
{
    using ART.Domotica.Worker.Consumers;
    using ART.Domotica.Worker.Consumers.Locale;
    using ART.Domotica.Worker.Consumers.SI;
    using ART.Domotica.Worker.IConsumers;
    using ART.Domotica.Worker.IConsumers.Locale;
    using ART.Domotica.Worker.IConsumers.SI;

    using Autofac;

    public class ConsumerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            //SI
            builder.RegisterType<NumericalScaleConsumer>().As<INumericalScaleConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<NumericalScalePrefixConsumer>().As<INumericalScalePrefixConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<NumericalScaleTypeConsumer>().As<INumericalScaleTypeConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<NumericalScaleTypeCountryConsumer>().As<INumericalScaleTypeCountryConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<UnitMeasurementConsumer>().As<IUnitMeasurementConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<UnitMeasurementTypeConsumer>().As<IUnitMeasurementTypeConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<UnitMeasurementScaleConsumer>().As<IUnitMeasurementScaleConsumer>().SingleInstance().AutoActivate();

            builder.RegisterType<ApplicationConsumer>().As<IApplicationConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<ApplicationMQConsumer>().As<IApplicationMQConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<ApplicationUserConsumer>().As<IApplicationUserConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<ContinentConsumer>().As<IContinentConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<CountryConsumer>().As<ICountryConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<DeviceNTPConsumer>().As<IDeviceNTPConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<DSFamilyTempSensorConsumer>().As<IDSFamilyTempSensorConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<ESPDeviceConsumer>().As<IESPDeviceConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorChartLimiterConsumer>().As<ISensorChartLimiterConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorConsumer>().As<ISensorConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorDatasheetConsumer>().As<ISensorDatasheetConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorTriggerConsumer>().As<ISensorTriggerConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorTypeConsumer>().As<ISensorTypeConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorUnitMeasurementDefaultConsumer>().As<ISensorUnitMeasurementDefaultConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<SensorDatasheetUnitMeasurementScaleConsumer>().As<ISensorDatasheetUnitMeasurementScaleConsumer>().SingleInstance().AutoActivate();
            builder.RegisterType<TimeZoneConsumer>().As<ITimeZoneConsumer>().SingleInstance().AutoActivate();

            //builder.RegisterType<ApplicationConsumer>()
            //    .As<IApplicationConsumer>()
            //    .SingleInstance()
            //    .AutoActivate()
            //    .AsImplementedInterfaces()
            //    .EnableInterfaceInterceptors()
            //    .InterceptedBy(typeof(CallDebugLogger));

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