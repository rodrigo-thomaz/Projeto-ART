using ART.MQ.DistributedServices.IProducers;
using ART.MQ.DistributedServices.Producers;
using Autofac;

namespace ART.MQ.DistributedServices.Modules
{
    public class ProducerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorProducer>().As<IDSFamilyTempSensorProducer>();
            builder.RegisterType<TemperatureScaleProducer>().As<ITemperatureScaleProducer>();
        }
    }
}