using ART.Data.Domain.Interfaces;
using ART.Data.Domain.Services;
using Autofac;

namespace ART.Data.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorDomain>().As<IDSFamilyTempSensorDomain>();
            builder.RegisterType<TemperatureScaleDomain>().As<ITemperatureScaleDomain>();
        }
    }
}
