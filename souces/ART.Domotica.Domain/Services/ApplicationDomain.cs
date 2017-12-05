namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Domain;
    using ART.Domotica.Repository;
    using Autofac;
    using ART.Domotica.Repository.Repositories;
    using System;

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

        public async Task<Application> GetByKey(Guid applicationId)
        {
            var data =  await _applicationRepository.GetByKey(applicationId);

            if (data == null)
            {
                throw new Exception("Application not found");
            }

            return data;
        }

        #endregion Methods
    }
}