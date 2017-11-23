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

    public class ApplicationBrokerSettingDomain : DomainBase, IApplicationBrokerSettingDomain
    {
        #region Fields

        private readonly IApplicationBrokerSettingRepository _applicationBrokerSettingRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationBrokerSettingDomain(IComponentContext componentContext) 
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _applicationBrokerSettingRepository = new ApplicationBrokerSettingRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<ApplicationBrokerSetting> Get(AuthenticatedMessageContract message)
        {
            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);
            return await _applicationBrokerSettingRepository.GetById(applicationUserEntity.ApplicationId);
        }

        #endregion Methods
    }
}