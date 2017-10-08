namespace ART.Domotica.Domain.Services
{

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;

    public class HardwareDomain : IHardwareDomain
    {
        #region Fields

        private readonly IHardwareRepository _hardwareRepository;

        #endregion Fields

        #region Constructors

        public HardwareDomain(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }

        #endregion Constructors

        #region Methods

        

        #endregion Methods
    }
}