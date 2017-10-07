namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using System.Collections.Generic;
    using System;

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
            applicationUser.Application = new Application
            {
                CreateDate = DateTime.Now,
            };            
            
            await _applicationUserRepository.Insert(applicationUser);           
        }

        #endregion Methods
    }
}