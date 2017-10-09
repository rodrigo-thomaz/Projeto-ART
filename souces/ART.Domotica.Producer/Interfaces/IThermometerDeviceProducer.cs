using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;

namespace ART.Domotica.Producer.Interfaces
{
    public interface IThermometerDeviceProducer
    {
        Task GetList(AuthenticatedMessageContract message);
    }
}