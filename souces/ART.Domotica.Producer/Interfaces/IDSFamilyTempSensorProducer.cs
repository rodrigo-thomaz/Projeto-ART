namespace ART.Domotica.Producer.Interfaces
{
    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;
    using System.Threading.Tasks;

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