namespace ART.Domotica.Worker.IConsumers
{
    using System.Threading.Tasks;

    using RabbitMQ.Client.Events;

    public interface IApplicationConsumer
    {
        #region Methods

        void Initialize();

        void GetReceived(object sender, BasicDeliverEventArgs e);

        Task GetReceivedAsync(object sender, BasicDeliverEventArgs e);

        #endregion Methods
    }
}