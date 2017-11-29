namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorTriggerProducer
    {
        #region Methods

        Task SetAlarmBuzzerOn(AuthenticatedMessageContract<SensorTriggerSetAlarmBuzzerOnRequestContract> message);

        Task SetAlarmCelsius(AuthenticatedMessageContract<SensorTriggerSetAlarmCelsiusRequestContract> message);

        Task SetAlarmOn(AuthenticatedMessageContract<SensorTriggerSetAlarmOnRequestContract> message);

        #endregion Methods
    }
}