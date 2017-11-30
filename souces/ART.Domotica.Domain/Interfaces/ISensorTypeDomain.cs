namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorTypeDomain
    {
        #region Methods

        Task<List<SensorType>> GetAll();

        #endregion Methods
    }
}