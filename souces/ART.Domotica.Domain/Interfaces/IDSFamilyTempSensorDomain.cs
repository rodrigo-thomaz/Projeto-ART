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

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);        

        #endregion Methods
    }
}