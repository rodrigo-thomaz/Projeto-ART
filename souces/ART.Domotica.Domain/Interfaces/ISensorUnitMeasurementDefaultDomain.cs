namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorUnitMeasurementDefaultDomain
    {
        #region Methods

        Task<List<SensorUnitMeasurementDefault>> GetAll();

        #endregion Methods
    }
}