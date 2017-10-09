namespace ART.Domotica.WebApi.Modules
{
    using ART.Domotica.WebApi.Controllers;

    using Autofac;
    using Autofac.Integration.WebApi;

    public class ControllerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(ApplicationController).Assembly);
            builder.RegisterApiControllers(typeof(ApplicationUserController).Assembly);
            builder.RegisterApiControllers(typeof(DashboardController).Assembly);
            builder.RegisterApiControllers(typeof(DSFamilyTempSensorController).Assembly);
            builder.RegisterApiControllers(typeof(HardwaresInApplicationController).Assembly);
            builder.RegisterApiControllers(typeof(TemperatureScaleController).Assembly);
            builder.RegisterApiControllers(typeof(ThermometerDeviceController).Assembly);
        }

        #endregion Methods
    }
}