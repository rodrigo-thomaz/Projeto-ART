namespace ART.Domotica.Repository.Interfaces.Globalization
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.Globalization;
    using ART.Infra.CrossCutting.Repository;

    public interface ITimeZoneRepository : IRepository<ARTDbContext, TimeZone, byte>
    {
        #region Methods

        Task<List<TimeZone>> GetAll();

        #endregion Methods
    }
}