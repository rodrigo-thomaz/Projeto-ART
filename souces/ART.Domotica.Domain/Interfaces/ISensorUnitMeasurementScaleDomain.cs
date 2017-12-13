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

        Task<SensorUnitMeasurementScale> SetUnitMeasurementNumericalScaleTypeCountry(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId, NumericalScalePrefixEnum numericalScalePrefixId, NumericalScaleTypeEnum numericalScaleTypeId, short countryId);

        Task<SensorUnitMeasurementScale> SetValue(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal value);

        #endregion Methods
    }
}