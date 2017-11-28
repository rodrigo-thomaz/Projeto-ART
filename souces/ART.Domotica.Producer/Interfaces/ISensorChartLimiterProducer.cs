namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorChartLimiterProducer
    {
        #region Methods

        Task SetValue(AuthenticatedMessageContract<SensorChartLimiterSetValueRequestContract> message);

        #endregion Methods
    }
}