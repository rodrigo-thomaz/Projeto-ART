namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorChartLimiterDomain
    {
        #region Methods

        Task<SensorChartLimiter> SetValue(Guid sensorChartLimiterId, SensorChartLimiterPositionEnum position, decimal value);

        #endregion Methods
    }
}