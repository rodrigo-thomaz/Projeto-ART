namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAllResolutions(AuthenticatedMessageContract message);

        Task GetList(AuthenticatedMessageContract message);

        Task GetListInApplication(AuthenticatedMessageContract message);

        Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmContract> message);

        Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message);

        #endregion Methods
    }
}