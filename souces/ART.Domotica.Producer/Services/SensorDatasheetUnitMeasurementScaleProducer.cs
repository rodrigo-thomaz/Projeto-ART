﻿using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class SensorDatasheetUnitMeasurementScaleProducer : ProducerBase, ISensorDatasheetUnitMeasurementScaleProducer
    {
        #region constructors

        public SensorDatasheetUnitMeasurementScaleProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(SensorDatasheetUnitMeasurementScaleConstants.GetAllQueueName, message);
        }

        #endregion
    }
}