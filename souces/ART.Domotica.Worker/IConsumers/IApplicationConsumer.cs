using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IApplicationConsumer
    {
        void GetReceived(object sender, BasicDeliverEventArgs e);
        Task GetReceivedAsync(object sender, BasicDeliverEventArgs e);
    }
}