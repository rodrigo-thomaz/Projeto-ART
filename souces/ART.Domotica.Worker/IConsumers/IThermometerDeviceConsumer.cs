using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IThermometerDeviceConsumer
    {
        void GetListReceived(object sender, BasicDeliverEventArgs e);
        Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e);
    }
}