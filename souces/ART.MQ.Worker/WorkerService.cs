using MassTransit;
using Topshelf;

namespace ART.MQ.Worker
{
    public class WorkerService
    {
        private readonly IBusControl _busControl;

        public WorkerService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public bool Start()
        {
            _busControl.Start();
            return _busControl != null;
        }

        public bool Stop()
        {
            _busControl.Stop();
            return _busControl != null;
        }
    }
}
