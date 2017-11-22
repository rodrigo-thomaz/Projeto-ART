namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId);

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<DSFamilyTempSensor> SetAlarmBuzzerOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract> message);

        Task<DSFamilyTempSensor> SetAlarmCelsius(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmCelsiusRequestContract> message);

        Task<DSFamilyTempSensor> SetAlarmOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract> message);

        Task<DSFamilyTempSensor> SetChartLimiterCelsius(AuthenticatedMessageContract<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract> message);

        Task<DSFamilyTempSensor> SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        Task<DSFamilyTempSensor> SetScale(AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract> message);

        Task<DSFamilyTempSensor> SetLabel(AuthenticatedMessageContract<DSFamilyTempSensorSetLabelRequestContract> message);

        #endregion Methods
    }
}