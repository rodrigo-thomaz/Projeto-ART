namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorUnitMeasurementScaleProducer
    {
        #region Methods

        Task SetValue(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract> message);

        #endregion Methods
    }
}