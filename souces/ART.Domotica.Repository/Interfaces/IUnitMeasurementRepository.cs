namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IUnitMeasurementRepository
    {
        #region Methods

        Task<List<UnitMeasurement>> GetAll();

        Task<UnitMeasurement> GetByKey(UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId);

        #endregion Methods
    }
}