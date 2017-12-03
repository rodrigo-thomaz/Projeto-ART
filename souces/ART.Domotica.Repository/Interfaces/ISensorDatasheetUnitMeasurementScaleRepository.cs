namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorDatasheetUnitMeasurementScaleRepository
    {
        #region Methods

        Task<List<SensorDatasheetUnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}