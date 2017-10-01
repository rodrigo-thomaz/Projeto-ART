using ART.MQ.Common.Contracts;
using ART.MQ.Consumer.IDomain;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ART.MQ.Consumer.Consumers
{
    public class DSFamilyTempSensorSetResolutionConsumer : IConsumer<IDSFamilyTempSensorSetResolutionContract>
    {
        #region private fields

        private readonly IDSFamilyTempSensorDomain _dsFamilyTempSensorDomain;

        #endregion

        #region constructors

        public DSFamilyTempSensorSetResolutionConsumer(IDSFamilyTempSensorDomain dsFamilyTempSensorDomain)
        {
            _dsFamilyTempSensorDomain = dsFamilyTempSensorDomain;
        }

        #endregion

        #region public void

        public async Task Consume(ConsumeContext<IDSFamilyTempSensorSetResolutionContract> context)
        {
            await Console.Out.WriteLineAsync($"[DSFamilyTempSensor][SetResolution] deviceAddress:{context.Message.DeviceAddress} value: {context.Message.Value} ");            
            await _dsFamilyTempSensorDomain.SetResolution(context.Message.DeviceAddress, context.Message.Value);
            //await context.Publish("");
        } 

        #endregion
    }
}
