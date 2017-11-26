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
    using System;

    public class ApplicationMQDomain : DomainBase, IApplicationMQDomain
    {
        #region Fields

        private readonly IApplicationMQRepository _applicationMQRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationMQDomain(IComponentContext componentContext) 
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _applicationMQRepository = new ApplicationMQRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
        }        

        #endregion Constructors

        #region Methods

        public async Task<ApplicationMQ> GetByApplicationUserId(AuthenticatedMessageContract message)
        {
            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);
            return await _applicationMQRepository.GetById(applicationUserEntity.ApplicationId);
        }

        public async Task<ApplicationMQ> GetByDeviceId(Guid deviceId)
        {
            return await _applicationMQRepository.GetByDeviceId(deviceId);
        }

        #endregion Methods
    }
}