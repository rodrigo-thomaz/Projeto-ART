namespace ART.Domotica.WebApi.IProducers
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAll(AuthenticatedMessageContract message);

        Task GetResolutions(AuthenticatedMessageContract message);

        Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmContract> message);

        Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message);

        #endregion Methods
    }
}