using ART.DistributedServices.Application.Interfaces;
using System;
using System.Threading.Tasks;
using ART.DistributedServices.Application.Models;

namespace ART.DistributedServices.Application.AppSercices
{
    public class DSFamilyTempSensorAppService : IDSFamilyTempSensorAppService
    {
        #region public voids

        public async Task<DSFamilyTempSensorSetResolutionResponse> SetResolution(DSFamilyTempSensorSetResolutionRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
