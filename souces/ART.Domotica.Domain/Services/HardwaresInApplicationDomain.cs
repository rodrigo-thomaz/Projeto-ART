namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Model;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;

    public class HardwaresInApplicationDomain : IHardwaresInApplicationDomain
    {
        #region Fields

        private readonly IHardwaresInApplicationRepository _hardwaresInApplicationRepository;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationDomain(IHardwaresInApplicationRepository hardwaresInApplicationRepository)
        {
            _hardwaresInApplicationRepository = hardwaresInApplicationRepository;
        }

        #endregion Constructors

        #region Methods

        

        #endregion Methods
    }
}