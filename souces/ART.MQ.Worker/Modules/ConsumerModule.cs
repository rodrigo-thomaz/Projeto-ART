using ART.MQ.Worker.Consumers;
using Autofac;

namespace ART.MQ.Worker.Modules
{
    public class ConsumerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorConsumer>().SingleInstance();
            builder.RegisterType<TemperatureScaleConsumer>().SingleInstance();
        }
    }
}
