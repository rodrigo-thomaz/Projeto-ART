namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ICountryDomain
    {
        #region Methods

        Task<List<Country>> GetAll();

        #endregion Methods
    }
}