using ART.Data.Domain.Interfaces;
using ART.Data.Domain.Services;
using ART.Data.Repository.Interfaces;
using ART.Data.Repository.Repositories;
using ART.MQ.Worker.Consumers;
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
