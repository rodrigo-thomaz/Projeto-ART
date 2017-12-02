namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;

    public interface IUnitMeasurementRepository
    {
        #region Methods

        Task<List<UnitMeasurement>> GetAll();

        Task<UnitMeasurement> GetByKey(UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId);

        #endregion Methods
    }
}