namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorDatasheetUnitMeasurementScaleRepository : IRepository<ARTDbContext, SensorDatasheetUnitMeasurementScale>
    {
        #region Methods

        Task<List<SensorDatasheetUnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}