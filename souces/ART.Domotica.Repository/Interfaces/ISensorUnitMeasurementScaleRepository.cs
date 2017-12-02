namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorUnitMeasurementScaleRepository
    {
        #region Methods

        Task<List<SensorUnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}