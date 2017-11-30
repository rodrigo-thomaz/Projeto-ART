namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorTypeRepository : IRepository<ARTDbContext, SensorType, SensorTypeEnum>
    {
        #region Methods

        Task<List<SensorType>> GetAll();

        #endregion Methods
    }
}