namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;

    public class SensorInDeviceDomain : DomainBase, ISensorInDeviceDomain
    {
        #region Fields

        private readonly ISensorInDeviceRepository _sensorInDeviceRepository;

        #endregion Fields

        #region Constructors

        public SensorInDeviceDomain(ISensorInDeviceRepository sensorInDeviceRepository)
        {
            _sensorInDeviceRepository = sensorInDeviceRepository;
        }

        #endregion Constructors
    }
}