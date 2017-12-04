namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorUnitMeasurementScaleDomain
    {
        #region Methods

        Task<SensorUnitMeasurementScale> SetValue(Guid sensorUnitMeasurementScaleId, SensorUnitMeasurementScalePositionEnum position, decimal value);

        #endregion Methods
    }
}