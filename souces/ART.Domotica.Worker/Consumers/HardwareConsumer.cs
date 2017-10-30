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

        private void Initialize()
        {
            
        }

        #endregion Methods
    }
}