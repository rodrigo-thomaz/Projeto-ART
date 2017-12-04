namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorsInDeviceRepository
    {
        #region Methods

        Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}