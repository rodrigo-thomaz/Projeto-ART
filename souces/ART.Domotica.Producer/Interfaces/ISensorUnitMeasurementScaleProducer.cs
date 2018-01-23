namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorUnitMeasurementScaleProducer
    {
        #region Methods

        Task SetChartLimiter(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract> message);

        Task SetDatasheetUnitMeasurementScale(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetDatasheetUnitMeasurementScaleRequestContract> message);

        Task SetRange(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract> message);

        Task SetUnitMeasurementNumericalScaleTypeCountry(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetUnitMeasurementNumericalScaleTypeCountryRequestContract> message);

        #endregion Methods
    }
}