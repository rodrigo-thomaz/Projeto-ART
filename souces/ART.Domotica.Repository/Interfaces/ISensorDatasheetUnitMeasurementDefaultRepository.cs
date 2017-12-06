namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorDatasheetUnitMeasurementDefaultRepository : IRepository<ARTDbContext, SensorDatasheetUnitMeasurementDefault, SensorDatasheetEnum>
    {
        #region Methods

        Task<List<SensorDatasheetUnitMeasurementDefault>> GetAll();

        #endregion Methods
    }
}