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
    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.Utils;

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

        public async Task<List<ThermometerDeviceUpdatePinsContract>> UpdatePins()
        {
            var existingPins = await _thermometerDeviceRepository.GetExistingPins();
            var entities = await _thermometerDeviceRepository.GetThermometerDeviceNotInApplication();

            foreach (var item in entities)
            {
                var pin = RandonHelper.RandomString(4);
                while (existingPins.Contains(pin))
                {
                    pin = RandonHelper.RandomString(4);
                }
                item.Pin = pin;
            }
            await _thermometerDeviceRepository.Update(entities);

            var result = Mapper.Map<List<ThermometerDevice>, List<ThermometerDeviceUpdatePinsContract>>(entities);

            return result;
        }

        #endregion Methods
    }
}