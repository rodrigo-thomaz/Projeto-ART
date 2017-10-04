namespace ART.Data.Domain
{
    using ART.Data.Domain.Interfaces;
    using ART.Data.Domain.Services;

    using Autofac;

    public class DomainModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DSFamilyTempSensorDomain>().As<IDSFamilyTempSensorDomain>();
            builder.RegisterType<TemperatureScaleDomain>().As<ITemperatureScaleDomain>();
        }

        #endregion Methods
    }
}