using ART.Data.Repository.Interfaces;
using ART.Data.Repository.Repositories;
using Autofac;

namespace ART.Data.Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorRepository>().As<IDSFamilyTempSensorRepository>();
            builder.RegisterType<DSFamilyTempSensorResolutionRepository>().As<IDSFamilyTempSensorResolutionRepository>();
        }
    }
}
