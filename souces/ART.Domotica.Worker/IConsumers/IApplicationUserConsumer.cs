using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IApplicationUserConsumer
    {
        Task RegisterUserAsync(object sender, BasicDeliverEventArgs e);
        void RegisterUserReceived(object sender, BasicDeliverEventArgs e);
    }
}