namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAllResolutions(AuthenticatedMessageContract message);

        Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmRequestContract> message);

        Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmRequestContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        Task SetScale(AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract> message);

        #endregion Methods
    }
}