namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using System.Collections.Generic;

    public interface ISensorsInDeviceDomain
    {
        #region Methods

        Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}