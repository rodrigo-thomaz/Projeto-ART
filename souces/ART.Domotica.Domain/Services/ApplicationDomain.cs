namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Model;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Domain;
    using log4net;
    using ART.Infra.CrossCutting.Logging;

    public class ApplicationDomain : DomainBase, IApplicationDomain
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationDomain(ILogger logger, IApplicationRepository applicationRepository, IApplicationUserRepository applicationUserRepository) 
        {
            _logger = logger;
            _applicationRepository = applicationRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<ApplicationGetModel> Get(AuthenticatedMessageContract message)
        {
            _logger.Debug();
            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);
            var applicationEntity = await _applicationRepository.GetById(applicationUserEntity.ApplicationId);            
            var result = Mapper.Map<Application, ApplicationGetModel>(applicationEntity);
            return result;
        }

        #endregion Methods
    }
}