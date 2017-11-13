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
    using ART.Domotica.Repository;
    using Autofac;
    using ART.Domotica.Repository.Repositories;

    public class ApplicationDomain : DomainBase, IApplicationDomain
    {
        #region Fields

        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationDomain(IComponentContext componentContext) 
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _applicationRepository = new ApplicationRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<ApplicationGetModel> Get(AuthenticatedMessageContract message)
        {
            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);
            var applicationEntity = await _applicationRepository.GetById(applicationUserEntity.ApplicationId);            
            var result = Mapper.Map<Application, ApplicationGetModel>(applicationEntity);
            return result;
        }

        #endregion Methods
    }
}