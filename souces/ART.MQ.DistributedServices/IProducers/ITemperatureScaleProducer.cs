using System.Threading.Tasks;

namespace ART.MQ.DistributedServices.IProducers
{
    public interface ITemperatureScaleProducer
    {
        Task GetScales(string session);
    }
}