namespace ART.Domotica.WebApi.Modules
{
    using ART.Domotica.WebApi.IProducers;
    using ART.Domotica.WebApi.Producers;

    using Autofac;

    public class ProducerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorProducer>().As<IDSFamilyTempSensorProducer>();
            builder.RegisterType<TemperatureScaleProducer>().As<ITemperatureScaleProducer>();
        }

        #endregion Methods
    }
}