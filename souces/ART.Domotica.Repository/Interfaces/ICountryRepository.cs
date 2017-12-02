namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ICountryRepository : IRepository<ARTDbContext, Country, short>
    {
        #region Methods

        Task<List<Country>> GetAll();

        #endregion Methods
    }
}