namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorDatasheetUnitMeasurementScaleRepository : IRepository<ARTDbContext, SensorDatasheetUnitMeasurementScale>
    {
        #region Methods

        Task<List<SensorDatasheetUnitMeasurementScale>> GetAll();

        Task<SensorDatasheetUnitMeasurementScale> GetByKey(SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId, NumericalScalePrefixEnum numericalScalePrefixId, NumericalScaleTypeEnum numericalScaleTypeId);

        #endregion Methods
    }
}