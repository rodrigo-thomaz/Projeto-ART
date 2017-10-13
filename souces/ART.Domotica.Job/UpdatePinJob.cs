namespace ART.Domotica.Job
{
    using ART.Domotica.Domain.Interfaces;

    using Quartz;

    public class UpdatePinJob : IJob
    {
        #region Fields

        private readonly IHardwareDomain _hardwareDomain;

        #endregion Fields

        #region Constructors

        public UpdatePinJob(IHardwareDomain hardwareDomain)
        {
            _hardwareDomain = hardwareDomain;
        }

        #endregion Constructors

        #region Methods

        public void Execute(IJobExecutionContext context)
        {
            var task = _hardwareDomain.UpdatePins();

            task.Wait();

            var teste = task.Result;
        }

        #endregion Methods
    }
}