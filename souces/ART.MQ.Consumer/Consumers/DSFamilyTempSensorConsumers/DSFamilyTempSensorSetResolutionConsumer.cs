using ART.MQ.Common.Contracts.DSFamilyTempSensorContracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ART.MQ.Consumer.Consumers.DSFamilyTempSensorConsumers
{
    public class DSFamilyTempSensorSetResolutionConsumer : IConsumer<DSFamilyTempSensorSetResolutionContract>
    {
        public async Task Consume(ConsumeContext<DSFamilyTempSensorSetResolutionContract> context)
        {
            await Console.Out.WriteLineAsync($"[DSFamilyTempSensor][SetResolution] deviceAddress:{context.Message.DeviceAddress} value: {context.Message.Value} ");

            // update the customer address
        }
    }
}
