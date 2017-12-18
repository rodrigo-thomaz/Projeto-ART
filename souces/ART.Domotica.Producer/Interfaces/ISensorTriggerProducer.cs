namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorTriggerProducer
    {
        #region Methods

        Task Delete(AuthenticatedMessageContract<SensorTriggerDeleteRequestContract> message);

        Task Insert(AuthenticatedMessageContract<SensorTriggerInsertRequestContract> message);

        Task SetBuzzerOn(AuthenticatedMessageContract<SensorTriggerSetBuzzerOnRequestContract> message);

        Task SetTriggerOn(AuthenticatedMessageContract<SensorTriggerSetTriggerOnRequestContract> message);

        Task SetTriggerValue(AuthenticatedMessageContract<SensorTriggerSetTriggerValueRequestContract> message);

        #endregion Methods
    }
}