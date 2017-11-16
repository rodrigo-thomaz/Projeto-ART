namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAllResolutions(AuthenticatedMessageContract message);

        Task SetAlarmOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract> message);

        Task SetAlarmValue(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmValueRequestContract> message);

        Task SetAlarmBuzzerOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract> message);        

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        Task SetScale(AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract> message);

        #endregion Methods
    }
}