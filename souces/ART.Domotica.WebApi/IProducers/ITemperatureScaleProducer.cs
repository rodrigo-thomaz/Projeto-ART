namespace ART.Domotica.WebApi.IProducers
{
    using ART.Infra.CrossCutting.MQ;
    using System.Threading.Tasks;

    public interface ITemperatureScaleProducer
    {
        #region Methods

        Task GetScales(AuthenticatedContract contract);

        #endregion Methods
    }
}