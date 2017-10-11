using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.Consumers
{
    public interface IApplicationConsumer
    {
        void GetReceived(object sender, BasicDeliverEventArgs e);
        Task GetReceivedAsync(object sender, BasicDeliverEventArgs e);
    }
}