namespace ART.Domotica.Worker.IConsumers
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    using RabbitMQ.Client.Events;

    public interface IDSFamilyTempSensorConsumer
    {
        #region Methods

        void GetAllReceived(object sender, BasicDeliverEventArgs e);

        Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e);

        void GetAllResolutionsReceived(object sender, BasicDeliverEventArgs e);

        Task GetAllResolutionsReceivedAsync(object sender, BasicDeliverEventArgs e);

        void GetListReceived(object sender, BasicDeliverEventArgs e);

        Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e);

        Task SendSetResolutionToDevice(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message);

        void SetHighAlarmReceived(object sender, BasicDeliverEventArgs e);

        Task SetHighAlarmReceivedAsync(object sender, BasicDeliverEventArgs e);

        void SetLowAlarmReceived(object sender, BasicDeliverEventArgs e);

        Task SetLowAlarmReceivedAsync(object sender, BasicDeliverEventArgs e);

        void SetResolutionReceived(object sender, BasicDeliverEventArgs e);

        Task SetResolutionReceivedAsync(object sender, BasicDeliverEventArgs e);

        #endregion Methods
    }
}