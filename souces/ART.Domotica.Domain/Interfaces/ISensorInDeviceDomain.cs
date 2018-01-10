namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorInDeviceDomain
    {
        #region Methods

        Task<List<SensorInDevice>> GetAllByDeviceInApplicationId(Guid applicationId, Guid deviceId, DeviceDatasheetEnum deviceDatasheetId);

        Task<SensorInDevice> SetOrdination(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, short ordination);

        #endregion Methods
    }
}