using ART.MQ.Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ART.MQ.Consumer.Consumers
{
    public class DSFamilyTempSensorSetResolutionConsumer :
    IConsumer<DSFamilyTempSensorSetResolutionContract>
    {
        public async Task Consume(ConsumeContext<DSFamilyTempSensorSetResolutionContract> context)
        {
            await Console.Out.WriteLineAsync($"Updating customer: {context.Message.DeviceAddress}");

            // update the customer address
        }
    }
}
