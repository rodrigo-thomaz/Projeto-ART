namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IUnitMeasurementTypeDomain
    {
        #region Methods

        Task<List<UnitMeasurementType>> GetAll();

        #endregion Methods
    }
}