namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Enums;

    public interface ISensorTempDSFamilyDomain
    {
        #region Methods

        Task<List<SensorTempDSFamilyResolution>> GetAllResolutions();

        Task<SensorTempDSFamily> SetResolution(Guid sensorTempDSFamilyId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, byte sensorTempDSFamilyResolutionId);

        #endregion Methods
    }
}