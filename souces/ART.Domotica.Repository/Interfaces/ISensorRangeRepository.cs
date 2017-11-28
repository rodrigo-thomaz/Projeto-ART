namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorRangeRepository : IRepository<ARTDbContext, SensorRange, byte>
    {
        #region Methods

        Task<List<SensorRange>> GetAll();

        #endregion Methods
    }
}