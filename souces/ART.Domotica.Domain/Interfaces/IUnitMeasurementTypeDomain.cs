namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface IUnitMeasurementTypeDomain
    {
        #region Methods

        Task<List<UnitMeasurementType>> GetAll();

        #endregion Methods
    }
}