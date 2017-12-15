using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Interfaces
{
    public interface ISensorInDeviceDomain
    {
        Task<SensorInDevice> SetOrdination(Guid deviceSensorsId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, short ordination);
    }
}