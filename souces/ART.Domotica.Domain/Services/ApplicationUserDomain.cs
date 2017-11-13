namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using Autofac;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Repositories;

    public class ApplicationUserDomain : DomainBase, IApplicationUserDomain
    {
        #region Fields

        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public ApplicationUserDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _applicationUserRepository = new ApplicationUserRepository(context);        
        }

        #endregion Constructors

        #region Methods

        public async Task RegisterUser(ApplicationUser entity)
        {
            entity.Application = new Application
            {
                CreateDate = DateTime.Now,
            };

            entity.CreateDate = DateTime.Now;

            await _applicationUserRepository.Insert(entity);           
        }

        #endregion Methods
    }
}