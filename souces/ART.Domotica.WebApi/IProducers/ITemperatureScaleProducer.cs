namespace ART.Domotica.WebApi.IProducers
{
    using System.Threading.Tasks;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ITemperatureScaleProducer
    {
        #region Methods

        Task GetScales(AuthenticatedMessageContract message);

        #endregion Methods
    }
}