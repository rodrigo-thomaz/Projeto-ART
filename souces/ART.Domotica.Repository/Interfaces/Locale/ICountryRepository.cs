namespace ART.Domotica.Repository.Interfaces.Locale
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.Locale;
    using ART.Infra.CrossCutting.Repository;

    public interface ICountryRepository : IRepository<ARTDbContext, Country, short>
    {
        #region Methods

        Task<List<Country>> GetAll();

        #endregion Methods
    }
}