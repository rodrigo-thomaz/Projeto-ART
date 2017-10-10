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

    public class ThermometerDeviceDomain : DomainBase, IThermometerDeviceDomain
    {
        #region Fields

        private readonly ILog _log;
        private readonly IThermometerDeviceRepository _thermometerDeviceRepositor;

        #endregion Fields

        #region Constructors

        public ThermometerDeviceDomain(ILog log, IThermometerDeviceRepository thermometerDeviceRepositor)
        {
            _log = log;
            _thermometerDeviceRepositor = thermometerDeviceRepositor;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ThermometerDeviceGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            _log.Debug(message);

            var data = await _thermometerDeviceRepositor.GetList();
            var result = Mapper.Map<List<ThermometerDevice>, List<ThermometerDeviceGetListModel>>(data);
            return result;
        }

        #endregion Methods
    }
}