namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorUnitMeasurementScaleDomain
    {
        #region Methods

        Task<List<SensorUnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}