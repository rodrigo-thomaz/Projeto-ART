namespace ART.Domotica.Worker.IConsumers
{
    using System.Threading.Tasks;

    using RabbitMQ.Client.Events;

    public interface ITemperatureScaleConsumer
    {
        #region Methods

        void GetAllReceived(object sender, BasicDeliverEventArgs e);

        Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e);

        #endregion Methods
    }
}