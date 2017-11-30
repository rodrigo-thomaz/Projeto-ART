namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorDatasheetDomain
    {
        #region Methods

        Task<List<SensorDatasheet>> GetAll();

        #endregion Methods
    }
}