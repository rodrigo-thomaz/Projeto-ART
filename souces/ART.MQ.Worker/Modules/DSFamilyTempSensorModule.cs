using ART.MQ.Worker.Consumers.DSFamilyTempSensorConsumers;
using ART.MQ.Worker.Domain;
using ART.MQ.Worker.IDomain;
using ART.MQ.Worker.IRepositories;
using ART.MQ.Worker.Repositories;
using Autofac;

namespace ART.MQ.Worker.Modules
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
