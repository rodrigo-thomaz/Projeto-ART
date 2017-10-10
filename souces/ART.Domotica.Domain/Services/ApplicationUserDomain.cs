namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using System;
    using global::AutoMapper;
    using ART.Security.Contract;
    using log4net;
    using ART.Infra.CrossCutting.Domain;

    public class ApplicationUserDomain : DomainBase, IApplicationUserDomain
    {
        #region Fields

        private readonly ILog _log;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationUserDomain(ILog log, IApplicationUserRepository applicationUserRepository)
        {
            _log = log;
            _applicationUserRepository = applicationUserRepository;        
        }

        #endregion Constructors

        #region Methods

        public async Task RegisterUser(RegisterUserContract contract)
        {
            _log.Debug(contract);

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