namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;

    public class ApplicationDomain : IApplicationDomain
    {
        #region Fields

        private readonly IApplicationRepository _applicationRepository;

        #endregion Fields

        #region Constructors

        public ApplicationDomain(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        #endregion Constructors
    }
}