using MassTransit;

namespace ART.MQ.Consumer
{
    public class BusControlService
    {
        private readonly IBusControl _busControl;

        public BusControlService(IBusControl busControl)
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
