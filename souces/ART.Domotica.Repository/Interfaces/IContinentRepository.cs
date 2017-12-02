namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IContinentRepository : IRepository<ARTDbContext, Continent, ContinentEnum>
    {
        #region Methods

        Task<List<Continent>> GetAll();

        #endregion Methods
    }
}