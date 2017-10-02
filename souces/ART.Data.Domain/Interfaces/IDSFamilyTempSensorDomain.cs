using System;
using System.Threading.Tasks;

namespace ART.Data.Domain.Interfaces
{
    public interface IDSFamilyTempSensorDomain
    {
        Task SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);
        Task SetHighAlarm(Guid dsFamilyTempSensorId, decimal highAlarm);
        Task SetLowAlarm(Guid dsFamilyTempSensorId, decimal lowAlarm);
    }
}
