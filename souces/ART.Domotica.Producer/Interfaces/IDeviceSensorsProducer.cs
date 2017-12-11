using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;

namespace ART.Domotica.Producer.Interfaces
{
    public interface IDeviceSensorsProducer
    {
        Task SetPublishIntervalInSeconds(AuthenticatedMessageContract<DeviceSensorsSetPublishIntervalInSecondsRequestContract> message);
    }
}