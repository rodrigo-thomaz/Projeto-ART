namespace ART.Domotica.Worker.Jobs
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Worker.IConsumers;

    using Quartz;

    public class UpdatePinJob : IJob
    {
        #region Fields

        private readonly IHardwareConsumer _hardwareConsumer;
        private readonly IHardwareDomain _hardwareDomain;

        #endregion Fields

        #region Constructors

        public UpdatePinJob(IHardwareConsumer hardwareConsumer, IHardwareDomain hardwareDomain)
        {
            _hardwareConsumer = hardwareConsumer;
            _hardwareDomain = hardwareDomain;
        }

        #endregion Constructors

        #region Methods

        public void Execute(IJobExecutionContext context)
        {
            var task = _hardwareDomain.UpdatePins();
            task.Wait();
            var data = task.Result;
            _hardwareConsumer.UpdatePins(data);
        }

        #endregion Methods
    }
}