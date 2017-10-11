namespace ART.Domotica.Worker.IConsumers
{
    using System.Threading.Tasks;

    using RabbitMQ.Client.Events;

    public interface IApplicationUserConsumer
    {
        #region Methods

        Task RegisterUserAsync(object sender, BasicDeliverEventArgs e);

        void RegisterUserReceived(object sender, BasicDeliverEventArgs e);

        #endregion Methods
    }
}