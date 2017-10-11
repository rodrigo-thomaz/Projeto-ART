namespace ART.Domotica.Worker.IConsumers
{
    using System.Threading.Tasks;

    using RabbitMQ.Client.Events;

    public interface IThermometerDeviceConsumer
    {
        #region Methods

        void GetListReceived(object sender, BasicDeliverEventArgs e);

        Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e);

        #endregion Methods
    }
}