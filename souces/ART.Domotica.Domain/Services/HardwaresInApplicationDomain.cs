namespace ART.Domotica.Domain.Services
{

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;

    public class HardwaresInApplicationDomain : IHardwaresInApplicationDomain
    {
        #region Fields

        private readonly IHardwaresInApplicationRepository _hardwaresInApplicationRepository;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationDomain(IHardwaresInApplicationRepository hardwaresInApplicationRepository)
        {
            _hardwaresInApplicationRepository = hardwaresInApplicationRepository;
        }

        #endregion Constructors

        #region Methods

        

        #endregion Methods
    }
}