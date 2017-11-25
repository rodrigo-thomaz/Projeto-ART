namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ITimeZoneDomain
    {
        #region Methods

        Task<List<TimeZone>> GetAll();

        #endregion Methods
    }
}