using ART.Data.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Data.Domain.Interfaces
{
    public interface IDSFamilyTempSensorDomain
    {
        Task<List<DSFamilyTempSensorResolution>> GetResolutions();
        Task SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);
        Task SetHighAlarm(Guid dsFamilyTempSensorId, decimal highAlarm);
        Task SetLowAlarm(Guid dsFamilyTempSensorId, decimal lowAlarm);
        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);
    }
}
