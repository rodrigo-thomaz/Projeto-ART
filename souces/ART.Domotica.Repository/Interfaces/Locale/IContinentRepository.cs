namespace ART.Domotica.Repository.Interfaces.Locale
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.Locale;
    using ART.Domotica.Repository.Entities.Locale;
    using ART.Infra.CrossCutting.Repository;

    public interface IContinentRepository : IRepository<ARTDbContext, Continent, ContinentEnum>
    {
        #region Methods

        Task<List<Continent>> GetAll();

        #endregion Methods
    }
}