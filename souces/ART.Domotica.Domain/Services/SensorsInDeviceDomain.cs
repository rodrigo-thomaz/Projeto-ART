namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;

    public class SensorsInDeviceDomain : DomainBase, ISensorsInDeviceDomain
    {
        #region Fields

        private readonly ISensorsInDeviceRepository _sensorsInDeviceRepository;

        #endregion Fields

        #region Constructors

        public SensorsInDeviceDomain(ISensorsInDeviceRepository sensorsInDeviceRepository)
        {
            _sensorsInDeviceRepository = sensorsInDeviceRepository;
        }

        #endregion Constructors
    }
}