namespace ART.Domotica.Producer.Interfaces
{
    using ART.Infra.CrossCutting.MQ.Contract;
    using System.Threading.Tasks;

    public interface ITemperatureScaleProducer
    {
        #region Methods

        Task GetAll(AuthenticatedMessageContract message);

        #endregion Methods
    }
}