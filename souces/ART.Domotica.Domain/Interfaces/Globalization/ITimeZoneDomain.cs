namespace ART.Domotica.Domain.Interfaces.Globalization
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.Globalization;

    public interface ITimeZoneDomain
    {
        #region Methods

        Task<List<TimeZone>> GetAll();

        #endregion Methods
    }
}