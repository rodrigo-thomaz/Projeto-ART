using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class DeviceDatasheetDomain : DomainBase, IDeviceDatasheetDomain
    {
        #region private readonly fields

        private readonly IDeviceDatasheetRepository _deviceDatasheetRepository;

        #endregion

        #region constructors

        public DeviceDatasheetDomain(IDeviceDatasheetRepository deviceDatasheetRepository)
        {
            _deviceDatasheetRepository = deviceDatasheetRepository;
        }

        #endregion

        #region public voids

        public async Task<List<DeviceDatasheet>> GetAll()
        {
            return await _deviceDatasheetRepository.GetAll();
        }

        #endregion
    }
}
