﻿using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class SensorTempDSFamilyProducer : ProducerBase, ISensorTempDSFamilyProducer
    {
        #region constructors

        public SensorTempDSFamilyProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids  

        public async Task GetAllResolutions(AuthenticatedMessageContract message)
        {
            await BasicPublish(SensorTempDSFamilyConstants.GetAllResolutionsQueueName, message);
        }

        public async Task SetResolution(AuthenticatedMessageContract<SensorTempDSFamilySetResolutionRequestContract> message)
        {
            await BasicPublish(SensorTempDSFamilyConstants.SetResolutionQueueName, message);
        }        

        #endregion
    }
}