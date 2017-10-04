namespace ART.MQ.Worker.Modules
{
    using ART.MQ.Worker.Consumers;

    using Autofac;

    public class ConsumerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorConsumer>().SingleInstance();
            builder.RegisterType<TemperatureScaleConsumer>().SingleInstance();
        }

        #endregion Methods
    }
}