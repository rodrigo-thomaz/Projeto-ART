namespace ART.Data.Domain.Services
{
    using ART.Data.Domain.Interfaces;
    using ART.Data.Repository.Interfaces;

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