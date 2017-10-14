namespace ART.Domotica.Worker.Consumers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Worker.Contracts;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.MQ.Worker;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using RabbitMQ.Client;

    public class HardwareConsumer : ConsumerBase, IHardwareConsumer
    {
        #region Constructors

        public HardwareConsumer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        public void UpdatePins(List<HardwareUpdatePinsContract> contracts)
        {
            foreach (var contract in contracts)
            {
                var queueName = GetQueueName(contract.HardwareId);
                var deviceMessage = new DeviceMessageContract<HardwareUpdatePinsContract>(HardwareConstants.UpdatePinsQueueName, contract);
                var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                var buffer = Encoding.UTF8.GetBytes(json);
                _model.BasicPublish("", queueName, null, buffer);
            }
        }

        private string GetQueueName(Guid hardwareId)
        {
            var queueName = string.Format("mqtt-subscription-{0}qos0", hardwareId);
            return queueName;
        }

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: HardwareConstants.UpdatePinsQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);
        }

        #endregion Methods
    }
}