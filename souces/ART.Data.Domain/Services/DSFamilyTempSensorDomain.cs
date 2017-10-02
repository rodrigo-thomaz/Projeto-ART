using System.Threading.Tasks;
using System;
using ART.Data.Domain.Interfaces;
using ART.Data.Repository.Interfaces;

namespace ART.Data.Domain.Services
{
    public class DSFamilyTempSensorDomain : IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IDSFamilyTempSensorRepository dsFamilyTempSensorRepository)
        {
            _dsFamilyTempSensorRepository = dsFamilyTempSensorRepository;
        }

        #endregion

        #region public voids

        public async Task SetResolution(string deviceAddres, int value)
        {
            var entity = await _dsFamilyTempSensorRepository.GetByDeviceAddress(deviceAddres);

            if(entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            
        } 

        #endregion
    }
}
 