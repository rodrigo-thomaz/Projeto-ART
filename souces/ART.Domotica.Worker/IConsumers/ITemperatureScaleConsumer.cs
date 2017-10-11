using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.IConsumers
{
    public interface ITemperatureScaleConsumer
    {
        void GetAllReceived(object sender, BasicDeliverEventArgs e);
        Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e);
    }
}