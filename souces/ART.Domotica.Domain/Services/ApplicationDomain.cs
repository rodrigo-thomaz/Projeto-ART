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

        #endregion Fields

        #region Constructors

        public ApplicationDomain(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ApplicationGetAllModel>> GetAll(AuthenticatedMessageContract message)
        {
            var entities = await _applicationRepository.GetAll(message.ApplicationUserId);
            var models = Mapper.Map<List<Application>, List<ApplicationGetAllModel>>(entities);
            return models;
        }

        #endregion Methods
    }
}