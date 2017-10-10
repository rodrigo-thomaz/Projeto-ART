namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Interfaces;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    using log4net;
    using ART.Infra.CrossCutting.Domain;
    using ART.Infra.CrossCutting.Logging;

    public class ThermometerDeviceDomain : DomainBase, IThermometerDeviceDomain
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IThermometerDeviceRepository _thermometerDeviceRepositor;

        #endregion Fields

        #region Constructors

        public ThermometerDeviceDomain(ILogger logger, IThermometerDeviceRepository thermometerDeviceRepositor)
        {
            _logger = logger;
            _thermometerDeviceRepositor = thermometerDeviceRepositor;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ThermometerDeviceGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            _logger.Debug();

            var data = await _thermometerDeviceRepositor.GetList();
            var result = Mapper.Map<List<ThermometerDevice>, List<ThermometerDeviceGetListModel>>(data);
            return result;
        }

        #endregion Methods
    }
}