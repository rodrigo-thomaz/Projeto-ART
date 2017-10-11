using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IHardwaresInApplicationConsumer
    {
        void DeleteHardwareReceived(object sender, BasicDeliverEventArgs e);
        Task DeleteHardwareReceivedAsync(object sender, BasicDeliverEventArgs e);
        void GetListReceived(object sender, BasicDeliverEventArgs e);
        Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e);
        void InsertHardwareReceived(object sender, BasicDeliverEventArgs e);
        Task InsertHardwareReceivedAsync(object sender, BasicDeliverEventArgs e);
        void SearchPinReceived(object sender, BasicDeliverEventArgs e);
        Task SearchPinReceivedAsync(object sender, BasicDeliverEventArgs e);
    }
}