namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorsInDeviceDomain
    {
        #region Methods

        Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}