namespace ART.Domotica.Domain
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Domain.Services;

    using Autofac;

    public class DomainModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDomain>().As<IApplicationDomain>();
            builder.RegisterType<ApplicationUserDomain>().As<IApplicationUserDomain>();
            builder.RegisterType<DSFamilyTempSensorDomain>().As<IDSFamilyTempSensorDomain>();
            builder.RegisterType<TemperatureScaleDomain>().As<ITemperatureScaleDomain>();
        }

        #endregion Methods
    }
}