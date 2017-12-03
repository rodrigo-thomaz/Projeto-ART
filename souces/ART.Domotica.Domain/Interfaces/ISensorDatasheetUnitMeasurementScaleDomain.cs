namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorDatasheetUnitMeasurementScaleDomain
    {
        #region Methods

        Task<List<SensorDatasheetUnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}