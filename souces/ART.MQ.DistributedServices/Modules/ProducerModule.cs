namespace ART.MQ.DistributedServices.Modules
{
    using ART.MQ.DistributedServices.IProducers;
    using ART.MQ.DistributedServices.Producers;

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