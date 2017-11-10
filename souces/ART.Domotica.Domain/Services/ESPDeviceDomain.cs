namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Contract;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using ART.Infra.CrossCutting.Utils;

    public class ESPDeviceDomain : DomainBase, IESPDeviceDomain
    {
        #region Fields

        private readonly IESPDeviceRepository _espDeviceRepository;
        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IHardwareInApplicationRepository _hardwareInApplicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;                

        #endregion Fields

        #region Constructors

        public ESPDeviceDomain(IESPDeviceRepository espDeviceRepository, IDSFamilyTempSensorRepository dsFamilyTempSensorRepository, IApplicationUserRepository applicationUserRepository, IHardwareInApplicationRepository hardwareInApplicationRepository)
        {
            _espDeviceRepository = espDeviceRepository;
            _dsFamilyTempSensorRepository = dsFamilyTempSensorRepository;
            _applicationUserRepository = applicationUserRepository;
            _hardwareInApplicationRepository = hardwareInApplicationRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwareInApplication>> GetListInApplication(AuthenticatedMessageContract message)
        {
            return await _espDeviceRepository.GetListInApplication(message.ApplicationUserId);
        }

        public async Task<ESPDeviceBase> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message)
        {
            var data = await _espDeviceRepository.GetByPin(message.Contract.Pin);

            if (data == null)
            {
                throw new Exception("Pin not found");
            }
            return data;
        }

        public async Task<HardwareInApplication> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message)
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

            await _hardwareInApplicationRepository.Insert(hardwaresInApplicationEntity);

            var allSensorsThatAreNotInApplication = await _dsFamilyTempSensorRepository.GetAllThatAreNotInApplicationByDevice(hardwareEntity.Id);

            foreach (var item in allSensorsThatAreNotInApplication)
            {
                var hardwaresInApplicationSensorEntity = new HardwareInApplication
                {
                    ApplicationId = applicationUserEntity.ApplicationId,
                    HardwareBaseId = item.Id,
                    CreateByApplicationUserId = applicationUserEntity.Id,
                    CreateDate = DateTime.Now.ToUniversalTime(),
                };

                await _hardwareInApplicationRepository.Insert(hardwaresInApplicationSensorEntity);
            }            

            return hardwaresInApplicationEntity;
        }

        public async Task<HardwareInApplication> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message)
        {
            var entity = await _hardwareInApplicationRepository.GetById(message.Contract.HardwareInApplicationId);

            if (entity == null)
            {
                throw new Exception("HardwareInApplication not found");
            }

            await _hardwareInApplicationRepository.Delete(entity);

            return entity;
        }

        public async Task<List<ESPDeviceBase>> UpdatePins()
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

            return entities;
        }

        public async Task<ESPDeviceBase> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract)
        {
            var data = await _espDeviceRepository.GetDeviceInApplication(contract.ChipId, contract.FlashChipId, contract.MacAddress);            

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }
            
            return data;
        }

        #endregion Methods
    }
}