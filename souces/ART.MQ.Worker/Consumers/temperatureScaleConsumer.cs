using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.MQ;
using ART.Domotica.Common.QueueNames;
using ART.MQ.Worker.Models;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ART.MQ.Worker.Consumers
{
    public class TemperatureScaleConsumer
    {
        #region private fields

        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _getScalesConsumer;

        private readonly ITemperatureScaleDomain _temperatureScaleDomain;

        #endregion

        #region constructors

        public TemperatureScaleConsumer(IConnection connection, ITemperatureScaleDomain temperatureScaleDomain)
        {
            _connection = connection;

            _model = _connection.CreateModel();

            _getScalesConsumer = new EventingBasicConsumer(_model);

            _temperatureScaleDomain = temperatureScaleDomain;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: TemperatureScaleQueueNames.GetScalesQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _getScalesConsumer.Received += GetScalesReceived;

            _model.BasicConsume(TemperatureScaleQueueNames.GetScalesQueueName, false, _getScalesConsumer);
        }

        private void GetScalesReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetScalesReceivedAsync(sender, e));
        }

        private async Task GetScalesReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[TemperatureScaleConsumer.GetScalesReceivedAsync] ");
            _model.BasicAck(e.DeliveryTag, false);

            var session = SerializationHelpers.DeserializeJsonBufferToType<string>(e.Body);
            var exchange = string.Format("{0}-{1}", session, "GetScalesCompleted");

            var entities = await _temperatureScaleDomain.GetScales();
            Console.WriteLine("[TemperatureScaleDomain.GetScales] Ok");

            var models = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleModel>>(entities);
            var json = JsonConvert.SerializeObject(models, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("amq.topic", exchange, null, buffer);
        }

        #endregion
    }
}
