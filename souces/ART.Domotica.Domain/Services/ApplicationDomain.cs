namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Model;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;

    public class ApplicationDomain : IApplicationDomain
    {
        #region Fields

        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationDomain(IApplicationRepository applicationRepository, IApplicationUserRepository applicationUserRepository)
        {
            _applicationRepository = applicationRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<ApplicationGetModel> Get(AuthenticatedMessageContract message)
        {
            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);
            var applicationEntity = await _applicationRepository.GetById(applicationUserEntity.ApplicationId);            
            var model = Mapper.Map<Application, ApplicationGetModel>(applicationEntity);
            return model;
        }

        #endregion Methods
    }
}