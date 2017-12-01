namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAllResolutions(AuthenticatedMessageContract message);

        Task SetLabel(AuthenticatedMessageContract<DSFamilyTempSensorSetLabelRequestContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        Task SetUnitMeasurement(AuthenticatedMessageContract<DSFamilyTempSensorSetUnitMeasurementRequestContract> message);

        #endregion Methods
    }
}