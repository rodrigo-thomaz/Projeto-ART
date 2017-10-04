namespace ART.Data.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;

    public interface ITemperatureScaleRepository : IRepository<TemperatureScale, byte>
    {
        #region Methods

        Task<List<TemperatureScale>> GetAll();

        #endregion Methods
    }
}