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

    public class HardwareDomain : IHardwareDomain
    {
        #region Fields

        private readonly IHardwareRepository _hardwareRepository;

        #endregion Fields

        #region Constructors

        public HardwareDomain(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwareGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            var data = await _hardwareRepository.GetList();
            var result = Mapper.Map<List<HardwareBase>, List<HardwareGetListModel>>(data);
            return result;
        }

        #endregion Methods
    }
}