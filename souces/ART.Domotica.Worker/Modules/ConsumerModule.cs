namespace ART.Domotica.Worker.Modules
{
    using ART.Domotica.Worker.Consumers;

    using Autofac;

    public class ConsumerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserConsumer>().SingleInstance();
            builder.RegisterType<DSFamilyTempSensorConsumer>().SingleInstance();
            builder.RegisterType<TemperatureScaleConsumer>().SingleInstance();
        }

        #endregion Methods
    }
}