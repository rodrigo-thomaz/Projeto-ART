namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public interface IUnitMeasurementRepository : IRepository<ARTDbContext, UnitMeasurement>
    {
        #region Methods

        Task<List<UnitMeasurement>> GetAll();

        Task<UnitMeasurement> GetByKey(UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId);

        #endregion Methods
    }
}