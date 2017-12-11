using ART.Domotica.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Interfaces
{
    public interface IDeviceSensorsDomain
    {
        Task<DeviceSensors> SetPublishIntervalInSeconds(Guid deviceSensorsId, int publishIntervalInSeconds);        
    }
}