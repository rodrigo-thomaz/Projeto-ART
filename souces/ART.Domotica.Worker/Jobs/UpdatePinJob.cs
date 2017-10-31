namespace ART.Domotica.Worker.Jobs
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Worker.IConsumers;

    using Quartz;

    public class UpdatePinJob : IJob
    {
        #region Fields

        private readonly IESPDeviceConsumer _espDeviceConsumer;
        private readonly IESPDeviceDomain _espDeviceDomain;

        #endregion Fields

        #region Constructors

        public UpdatePinJob(IESPDeviceConsumer espDeviceConsumer, IESPDeviceDomain espDeviceDomain)
        {
            _espDeviceConsumer = espDeviceConsumer;
            _espDeviceDomain = espDeviceDomain;
        }

        #endregion Constructors

        #region Methods

        public void Execute(IJobExecutionContext context)
        {
            var task = _espDeviceDomain.UpdatePins();
            task.Wait();
            var data = task.Result;
            _espDeviceConsumer.UpdatePins(data);
        }

        #endregion Methods
    }
}