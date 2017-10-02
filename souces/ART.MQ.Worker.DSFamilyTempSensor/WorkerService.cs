using MassTransit;
using System.Threading.Tasks;

namespace ART.MQ.Worker.DSFamilyTempSensor
{
    public class WorkerService
    {
        private readonly IBusControl _busControl;

        public WorkerService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<bool> StartAsync()
        {
            await _busControl.StartAsync();
            return true;
        }

        public async Task<bool> StopAsync()
        {
            await _busControl.StopAsync();
            return true;
        }
    }
}
