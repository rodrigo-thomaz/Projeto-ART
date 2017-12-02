namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IContinentDomain
    {
        #region Methods

        Task<List<Continent>> GetAll();

        #endregion Methods
    }
}