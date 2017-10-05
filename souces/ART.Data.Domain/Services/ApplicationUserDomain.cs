namespace ART.Data.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Data.Domain.Interfaces;
    using ART.Data.Repository.Entities;
    using ART.Data.Repository.Interfaces;

    public class ApplicationUserDomain : IApplicationUserDomain
    {
        #region Fields

        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationUserDomain(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        #endregion Constructors

        #region Methods

        public Task RegisterUser(ApplicationUser applicationUser)
        {
            throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}