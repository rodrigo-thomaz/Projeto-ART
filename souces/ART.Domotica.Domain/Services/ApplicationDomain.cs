namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Domain;
    using ART.Domotica.Repository;
    using Autofac;
    using ART.Domotica.Repository.Repositories;

    public class ApplicationDomain : DomainBase, IApplicationDomain
    {
        #region Fields

        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IApplicationBrokerSettingRepository _applicationBrokerSettingRepository;

        #endregion Fields

        #region Constructors

        public ApplicationDomain(IComponentContext componentContext) 
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _applicationRepository = new ApplicationRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
            _applicationBrokerSettingRepository = new ApplicationBrokerSettingRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<Application> Get(AuthenticatedMessageContract message)
        {
            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);
            var result = await _applicationRepository.GetById(applicationUserEntity.ApplicationId);
            //Load Broker Settings
            await _applicationBrokerSettingRepository.GetById(applicationUserEntity.ApplicationId);
            return result;
        }

        #endregion Methods
    }
}