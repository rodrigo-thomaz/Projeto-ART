namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorDatasheetUnitMeasurementDefaultProducer
    {
        #region Methods

        Task GetAll(AuthenticatedMessageContract message);

        #endregion Methods
    }
}