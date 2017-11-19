namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ITempSensorRangeRepository : IRepository<ARTDbContext, TempSensorRange, byte>
    {
        #region Methods

        Task<List<TempSensorRange>> GetAll();

        #endregion Methods
    }
}