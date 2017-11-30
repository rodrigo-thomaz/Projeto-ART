namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorUnitOfMeasurementDefaultDomain
    {
        #region Methods

        Task<List<SensorUnitOfMeasurementDefault>> GetAll();

        #endregion Methods
    }
}