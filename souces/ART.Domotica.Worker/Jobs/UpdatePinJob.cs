namespace ART.Domotica.Worker.Jobs
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Worker.IConsumers;

    using Quartz;

    public class UpdatePinJob : IJob
    {
        #region Fields

        private readonly IThermometerDeviceConsumer _thermometerDeviceConsumer;
        private readonly IThermometerDeviceDomain _thermometerDeviceDomain;

        #endregion Fields

        #region Constructors

        public UpdatePinJob(IThermometerDeviceConsumer thermometerDeviceConsumer, IThermometerDeviceDomain thermometerDeviceDomain)
        {
            _thermometerDeviceConsumer = thermometerDeviceConsumer;
            _thermometerDeviceDomain = thermometerDeviceDomain;
        }

        #endregion Constructors

        #region Methods

        public void Execute(IJobExecutionContext context)
        {
            var task = _thermometerDeviceDomain.UpdatePins();
            task.Wait();
            var data = task.Result;
            _thermometerDeviceConsumer.UpdatePins(data);
        }

        #endregion Methods
    }
}