namespace ART.Domotica.Job
{
    using System.Threading.Tasks;

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
            Task.Run(() =>
            {
                _hardwareDomain.UpdatePins();
            });
        }

        #endregion Methods
    }
}