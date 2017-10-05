namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ITemperatureScaleRepository : IRepository<ARTDbContext, TemperatureScale, byte>
    {
        #region Methods

        Task<List<TemperatureScale>> GetAll();

        #endregion Methods
    }
}