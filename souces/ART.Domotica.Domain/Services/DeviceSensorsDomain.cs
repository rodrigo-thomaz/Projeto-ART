namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;

    public class DeviceSensorsDomain : DomainBase, IDeviceSensorsDomain
    {
        #region Fields

        private readonly IDeviceSensorsRepository _deviceSensorsRepository;

        #endregion Fields

        #region Constructors

        public DeviceSensorsDomain(IDeviceSensorsRepository deviceSensorsRepository)
        {
            _deviceSensorsRepository = deviceSensorsRepository;
        }

        #endregion Constructors
    }
}