namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IUnitMeasurementDomain
    {
        #region Methods

        Task<List<UnitMeasurement>> GetAll();

        #endregion Methods
    }
}