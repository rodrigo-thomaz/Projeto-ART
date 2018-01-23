namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities;

    public interface ISensorUnitMeasurementScaleDomain
    {
        #region Methods

        Task<SensorUnitMeasurementScale> SetChartLimiter(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal value);

        Task<SensorUnitMeasurementScale> SetDatasheetUnitMeasurementScale(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId, NumericalScalePrefixEnum numericalScalePrefixId, NumericalScaleTypeEnum numericalScaleTypeId);

        Task<SensorUnitMeasurementScale> SetRange(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal value);

        Task<SensorUnitMeasurementScale> SetUnitMeasurementNumericalScaleTypeCountry(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId, NumericalScalePrefixEnum numericalScalePrefixId, NumericalScaleTypeEnum numericalScaleTypeId, short countryId);

        #endregion Methods
    }
}