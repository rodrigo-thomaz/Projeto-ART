using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class DeviceTypeDomain : DomainBase, IDeviceTypeDomain
    {
        #region private readonly fields

        private readonly IDeviceTypeRepository _deviceTypeRepository;

        #endregion

        #region constructors

        public DeviceTypeDomain(IDeviceTypeRepository deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }

        #endregion

        #region public voids

        public async Task<List<DeviceType>> GetAll()
        {
            return await _deviceTypeRepository.GetAll();
        }

        #endregion
    }
}
