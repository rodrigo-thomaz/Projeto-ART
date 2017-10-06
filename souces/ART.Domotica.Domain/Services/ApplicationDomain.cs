namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;

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

        #region public voids

        public Task<List<Application>> GetAll(AuthenticatedMessageContract message)
        {
            throw new System.Exception();
        } 

        #endregion
    }
}