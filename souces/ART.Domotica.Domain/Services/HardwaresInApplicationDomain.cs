namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Interfaces;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

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

        public async Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            var data = await _hardwaresInApplicationRepository.GetList();
            var result = Mapper.Map<List<HardwaresInApplication>, List<HardwaresInApplicationGetListModel>>(data);
            return result;
        }

        #endregion Methods
    }
}