namespace ART.Domotica.Worker.IConsumers
{
    using System.Threading.Tasks;

    using RabbitMQ.Client.Events;

    public interface IHardwaresInApplicationConsumer
    {
        #region Methods

        void DeleteHardwareReceived(object sender, BasicDeliverEventArgs e);

        Task DeleteHardwareReceivedAsync(object sender, BasicDeliverEventArgs e);

        void GetListReceived(object sender, BasicDeliverEventArgs e);

        Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e);

        void InsertHardwareReceived(object sender, BasicDeliverEventArgs e);

        Task InsertHardwareReceivedAsync(object sender, BasicDeliverEventArgs e);

        void SearchPinReceived(object sender, BasicDeliverEventArgs e);

        Task SearchPinReceivedAsync(object sender, BasicDeliverEventArgs e);

        #endregion Methods
    }
}