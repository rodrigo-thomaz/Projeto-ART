namespace ART.Domotica.Worker.Jobs
{
    using ART.Domotica.Worker.IConsumers;

    using Quartz;

    public class UpdatePinJob : IJob
    {
        #region Fields

        private readonly IESPDeviceConsumer _espDeviceConsumer;

        #endregion Fields

        #region Constructors

        public UpdatePinJob(IESPDeviceConsumer espDeviceConsumer)
        {
            _espDeviceConsumer = espDeviceConsumer;
        }

        #endregion Constructors

        #region Methods

        public void Execute(IJobExecutionContext context)
        {
            if(context.NextFireTimeUtc.HasValue)
            {
                _espDeviceConsumer.UpdatePins(context.NextFireTimeUtc.Value);
            }
        }

        #endregion Methods
    }
}