namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorUnitOfMeasurementDefaultRepository : IRepository<ARTDbContext, SensorUnitOfMeasurementDefault, SensorDatasheetEnum>
    {
        #region Methods

        Task<List<SensorUnitOfMeasurementDefault>> GetAll();

        #endregion Methods
    }
}