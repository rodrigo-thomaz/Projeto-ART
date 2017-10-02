using Autofac;

namespace ART.MQ.Worker.DSFamilyTempSensor
{
    public class DSFamilyTempSensorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<DSFamilyTempSensorSetResolutionConsumer>();
        }
    }
}
