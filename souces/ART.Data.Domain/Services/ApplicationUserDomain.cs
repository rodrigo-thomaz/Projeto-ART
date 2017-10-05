namespace ART.Data.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Data.Domain.Interfaces;
    using ART.Data.Repository.Entities;
    using ART.Data.Repository.Interfaces;
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
                Name = "Meu espaço de domótica",
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