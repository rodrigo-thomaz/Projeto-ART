namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IUnitOfMeasurementTypeDomain
    {
        #region Methods

        Task<List<UnitOfMeasurementType>> GetAll();

        #endregion Methods
    }
}