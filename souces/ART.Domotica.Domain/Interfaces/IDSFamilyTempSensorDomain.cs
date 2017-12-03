namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId);

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);

        Task<DSFamilyTempSensor> SetUnitMeasurement(Guid dsFamilyTempSensorId, UnitMeasurementEnum unitMeasurementId);

        #endregion Methods
    }
}