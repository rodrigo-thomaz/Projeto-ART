namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAllResolutions(AuthenticatedMessageContract message);

        Task SetAlarmBuzzerOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract> message);

        Task SetAlarmCelsius(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmCelsiusRequestContract> message);

        Task SetAlarmOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract> message);

        Task SetChartLimiterCelsius(AuthenticatedMessageContract<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract> message);

        Task SetLabel(AuthenticatedMessageContract<DSFamilyTempSensorSetLabelRequestContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        Task SetUnitOfMeasurement(AuthenticatedMessageContract<DSFamilyTempSensorSetUnitOfMeasurementRequestContract> message);

        #endregion Methods
    }
}