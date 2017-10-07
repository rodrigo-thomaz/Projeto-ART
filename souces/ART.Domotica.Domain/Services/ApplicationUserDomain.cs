namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using System;
    using ART.Security.Common.Contracts;
    using global::AutoMapper;

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

        public async Task RegisterUser(RegisterUserContract contract)
        {
            var applicationEntity = new Application
            {
                CreateDate = DateTime.Now,
            };

            var applicationUserEntity = Mapper.Map<RegisterUserContract, ApplicationUser>(contract);

            applicationUserEntity.Application = applicationEntity;
            applicationUserEntity.CreateDate = DateTime.Now;

            await _applicationUserRepository.Insert(applicationUserEntity);           
        }

        #endregion Methods
    }
}