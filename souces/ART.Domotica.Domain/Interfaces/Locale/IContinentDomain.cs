namespace ART.Domotica.Domain.Interfaces.Locale
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.Locale;

    public interface IContinentDomain
    {
        #region Methods

        Task<List<Continent>> GetAll();

        #endregion Methods
    }
}