﻿using ART.Domotica.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using System.Collections.Generic;
using Autofac;
using AutoMapper;
using ART.Infra.CrossCutting.Logging;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Domain.Interfaces.SI;
using ART.Domotica.Constant.SI;
using ART.Domotica.Model.SI;
using ART.Domotica.Worker.IConsumers.SI;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Worker.Consumers.SI
{
    public class UnitMeasurementTypeConsumer : ConsumerBase, IUnitMeasurementTypeConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;

        #endregion

        #region constructors

        public UnitMeasurementTypeConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(UnitMeasurementTypeConstants.GetAllQueueName);

            _getAllConsumer.Received += GetAllReceived;

            _model.BasicConsume(UnitMeasurementTypeConstants.GetAllQueueName, false, _getAllConsumer);
        }

        public void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        public async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IUnitMeasurementTypeDomain>();
            var data = await domain.GetAll();

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<UnitMeasurementType>, List<UnitMeasurementTypeGetModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, UnitMeasurementTypeConstants.GetAllCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
