namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorDatasheetUnitMeasurementDefaultDomain
    {
        #region Methods

        Task<List<SensorDatasheetUnitMeasurementDefault>> GetAll();

        #endregion Methods
    }
}