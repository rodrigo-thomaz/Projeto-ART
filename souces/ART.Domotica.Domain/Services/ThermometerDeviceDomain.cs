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
    using ART.Infra.CrossCutting.Domain;

    public class ThermometerDeviceDomain : DomainBase, IThermometerDeviceDomain
    {
        #region Fields

        private readonly IThermometerDeviceRepository _thermometerDeviceRepository;

        #endregion Fields

        #region Constructors

        public ThermometerDeviceDomain(IThermometerDeviceRepository thermometerDeviceRepository)
        {
            _thermometerDeviceRepository = thermometerDeviceRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ThermometerDeviceGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            var data = await _thermometerDeviceRepository.GetList();
            var result = Mapper.Map<List<ThermometerDevice>, List<ThermometerDeviceGetListModel>>(data);
            return result;
        }        

        #endregion Methods
    }
}