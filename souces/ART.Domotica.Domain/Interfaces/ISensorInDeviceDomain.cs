namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorInDeviceDomain
    {
        #region Methods

        Task<SensorInDevice> SetOrdination(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, short ordination);

        #endregion Methods
    }
}