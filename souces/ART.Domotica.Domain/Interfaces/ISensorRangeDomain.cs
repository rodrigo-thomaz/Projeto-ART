namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorRangeDomain
    {
        #region Methods

        Task<List<SensorRange>> GetAll();

        #endregion Methods
    }
}