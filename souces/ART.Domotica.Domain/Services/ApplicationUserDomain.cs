namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using System.Collections.Generic;

    public class ApplicationUserDomain : IApplicationUserDomain
    {
        #region Fields
        
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IApplicationRepository _applicationRepository;

        #endregion Fields

        #region Constructors

        public ApplicationUserDomain(IApplicationUserRepository applicationUserRepository, IApplicationRepository applicationRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _applicationRepository = applicationRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task RegisterUser(ApplicationUser applicationUser)
        {
            var application = new Application
            {
                UsersInApplication = new List<UsersInApplication>
                {
                    new UsersInApplication
                    {
                        ApplicationUser = applicationUser
                    }
                }
            };
            
            await _applicationRepository.Insert(application);           
        }

        #endregion Methods
    }
}