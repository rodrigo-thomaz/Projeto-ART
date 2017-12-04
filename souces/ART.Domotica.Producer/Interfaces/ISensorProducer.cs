namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorProducer
    {
        #region Methods

        Task GetAllByApplicationId(AuthenticatedMessageContract message);

        Task SetLabel(AuthenticatedMessageContract<SensorSetLabelRequestContract> message);

        Task SetUnitMeasurement(AuthenticatedMessageContract<SensorSetUnitMeasurementRequestContract> message);

        #endregion Methods
    }
}