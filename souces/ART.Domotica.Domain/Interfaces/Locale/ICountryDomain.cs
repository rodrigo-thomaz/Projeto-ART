namespace ART.Domotica.Domain.Interfaces.Locale
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.Locale;

    public interface ICountryDomain
    {
        #region Methods

        Task<List<Country>> GetAll();

        #endregion Methods
    }
}