namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IUnitOfMeasurementDomain
    {
        #region Methods

        Task<List<UnitOfMeasurement>> GetAll();

        #endregion Methods
    }
}