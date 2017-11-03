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
    using ART.Domotica.Contract;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using ART.Infra.CrossCutting.Utils;
    using ART.Infra.CrossCutting.Setting;
    using ART.Domotica.Constant;

    public class ESPDeviceDomain : DomainBase, IESPDeviceDomain
    {
        #region Fields

        private readonly IESPDeviceRepository _espDeviceRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISettingManager _settingsManager;

        #endregion Fields

        #region Constructors

        public ESPDeviceDomain(IESPDeviceRepository espDeviceRepository, IApplicationUserRepository applicationUserRepository, ISettingManager settingsManager)
        {
            _espDeviceRepository = espDeviceRepository;
            _applicationUserRepository = applicationUserRepository;
            _settingsManager = settingsManager;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ESPDeviceGetListModel>> GetListInApplication(AuthenticatedMessageContract message)
        {
            var data = await _espDeviceRepository.GetListInApplication(message.ApplicationUserId);
            var result = Mapper.Map<List<HardwareInApplication>, List<ESPDeviceGetListModel>>(data);
            return result;
        }

        public async Task<ESPDeviceGetByPinModel> GetByPin(AuthenticatedMessageContract<ESPDevicePinContract> message)
        {
            var data = await _espDeviceRepository.GetByPin(message.Contract.Pin);

            if (data == null)
            {
                throw new Exception("Pin not found");
            }

            var result = Mapper.Map<HardwareBase, ESPDeviceGetByPinModel>(data);

            return result;
        }

        public async Task InsertInApplication(AuthenticatedMessageContract<ESPDevicePinContract> message)
        {
            var hardwareEntity = await _espDeviceRepository.GetByPin(message.Contract.Pin);

            if (hardwareEntity == null)
            {
                throw new Exception("Pin not found");
            }

            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);

            if (hardwareEntity == null)
            {
                throw new Exception("ApplicationUser not found");
            }

            var hardwaresInApplicationEntity = new HardwareInApplication
            {
                ApplicationId = applicationUserEntity.ApplicationId,
                HardwareBaseId = hardwareEntity.Id,
                CreateByApplicationUserId = applicationUserEntity.Id,
                CreateDate = DateTime.Now.ToUniversalTime(),
            };

            await _espDeviceRepository.InsertInApplication(hardwaresInApplicationEntity);
        }

        public async Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationContract> message)
        {
            var hardwareEntity = await _espDeviceRepository.GetInApplicationById(message.Contract.HardwareInApplicationId);

            if (hardwareEntity == null)
            {
                throw new Exception("HardwareInApplication not found");
            }

            await _espDeviceRepository.DeleteFromApplication(hardwareEntity);
        }

        public async Task<List<ESPDeviceUpdatePinsContract>> UpdatePins()
        {
            var existingPins = await _espDeviceRepository.GetExistingPins();
            var entities = await _espDeviceRepository.GetESPDevicesNotInApplication();

            foreach (var item in entities)
            {
                var pin = RandonHelper.RandomString(4);
                while (existingPins.Contains(pin))
                {
                    pin = RandonHelper.RandomString(4);
                }
                item.Pin = pin;
            }
            await _espDeviceRepository.Update(entities);

            var result = Mapper.Map<List<ESPDeviceBase>, List<ESPDeviceUpdatePinsContract>>(entities);

            return result;
        }

        public async Task<ESPDeviceGetConfigurationsResponseContract> GetConfigurations(ESPDeviceGetConfigurationsRequestContract contract)
        {
            var data = await _espDeviceRepository.GetDeviceInApplication(contract.ChipId, contract.FlashChipId, contract.MacAddress);

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }

            var brokerHost = await _settingsManager.GetValueAsync<string>(SettingsConstants.BrokerHostSettingsKey);
            var brokerPort = await _settingsManager.GetValueAsync<int>(SettingsConstants.BrokerPortSettingsKey);
            var brokerUser = await _settingsManager.GetValueAsync<string>(SettingsConstants.BrokerUserSettingsKey);
            var brokerPwd = await _settingsManager.GetValueAsync<string>(SettingsConstants.BrokerPwdSettingsKey);

            var result = new ESPDeviceGetConfigurationsResponseContract
            {
                BrokerHost = brokerHost,
                BrokerPort = brokerPort,
                BrokerUser = brokerUser,
                BrokerPassword = brokerPwd,
            };

            Mapper.Map(data, result);

            return result;
        }

        #endregion Methods
    }
}