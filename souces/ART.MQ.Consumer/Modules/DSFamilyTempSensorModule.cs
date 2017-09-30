using ART.MQ.Consumer.Consumers.DSFamilyTempSensorConsumers;
using ART.MQ.Consumer.Domain;
using ART.MQ.Consumer.IDomain;
using ART.MQ.Consumer.IRepositories;
using ART.MQ.Consumer.Repositories;
using Autofac;

namespace ART.MQ.Consumer.Modules
{
    public class DSFamilyTempSensorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorRepository>().As<IDSFamilyTempSensorRepository>();
            builder.RegisterType<DSFamilyTempSensorDomain>().As<IDSFamilyTempSensorDomain>();
            builder.RegisterType<DSFamilyTempSensorSetResolutionConsumer>();
        }
    }
}
