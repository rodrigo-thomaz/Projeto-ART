namespace ART.MQ.DistributedServices.IProducers
{
    using System.Threading.Tasks;

    public interface ITemperatureScaleProducer
    {
        #region Methods

        Task GetScales(string session);

        #endregion Methods
    }
}