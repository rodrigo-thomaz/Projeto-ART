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
    using System.Transactions;
    using ART.Domotica.Domain.DTOs;
    using global::AutoMapper;

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

        public async Task<List<ESPDeviceBaseDTO>> GetListInApplication(AuthenticatedMessageContract message)
        {
            var data = await _espDeviceRepository.GetListInApplication(message.ApplicationUserId);
            return Mapper.Map<List<ESPDeviceBase>, List<ESPDeviceBaseDTO>>(data);
        }

        public async Task<ESPDeviceBaseDTO> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message)
        {
            var data = await _espDeviceRepository.GetByPin(message.Contract.Pin);

            if (data == null)
            {
                throw new Exception("Pin not found");
            }

            return Mapper.Map<ESPDeviceBase, ESPDeviceBaseDTO>(data);
        }

        public async Task<ESPDeviceBaseDTO> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message)
        {
            var hardwareEntity = await _espDeviceRepository.GetByPin(message.Contract.Pin);

            if (hardwareEntity == null)
            {
                throw new Exception("Pin not found");
            }

            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);

            if (applicationUserEntity == null)
            {
                throw new Exception("ApplicationUser not found");
            }

            var entitiesToInsert = new List<HardwareInApplication>();

            if (hardwareEntity.HardwaresInApplication == null)
                hardwareEntity.HardwaresInApplication = new List<HardwareInApplication>();

            hardwareEntity.HardwaresInApplication.Add(new HardwareInApplication
            {
                ApplicationId = applicationUserEntity.ApplicationId,
                HardwareBaseId = hardwareEntity.Id,
                HardwareBase = hardwareEntity,
                CreateByApplicationUserId = applicationUserEntity.Id,
                CreateDate = DateTime.Now.ToUniversalTime(),
            });

            //entitiesToInsert.Add(new HardwareInApplication
            //{
            //    ApplicationId = applicationUserEntity.ApplicationId,
            //    HardwareBaseId = hardwareEntity.Id,
            //    HardwareBase = hardwareEntity,
            //    CreateByApplicationUserId = applicationUserEntity.Id,
            //    CreateDate = DateTime.Now.ToUniversalTime(),
            //});

            await _espDeviceRepository.Update(hardwareEntity);

            var allSensorsThatAreNotInApplication = await _dsFamilyTempSensorRepository.GetAllThatAreNotInApplicationByDevice(hardwareEntity.Id);

            foreach (var item in allSensorsThatAreNotInApplication)
            {
                entitiesToInsert.Add(new HardwareInApplication
                {
                    ApplicationId = applicationUserEntity.ApplicationId,
                    HardwareBaseId = item.Id,
                    CreateByApplicationUserId = applicationUserEntity.Id,
                    CreateDate = DateTime.Now.ToUniversalTime(),
                });                
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _hardwareInApplicationRepository.Insert(entitiesToInsert);
                scope.Complete();
            }

            return Mapper.Map<ESPDeviceBase, ESPDeviceBaseDTO>(hardwareEntity);
        }

        public async Task<ESPDeviceBaseDTO> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message)
        {
            var hardwareInApplicationEntity = await _hardwareInApplicationRepository.GetById(message.Contract.HardwareInApplicationId);
            
            if (hardwareInApplicationEntity == null)
            {
                throw new Exception("HardwareInApplication not found");
            }

            await _hardwareInApplicationRepository.Delete(hardwareInApplicationEntity);

            var hardwareEntity = await _espDeviceRepository.GetById(hardwareInApplicationEntity.HardwareBaseId);

            return Mapper.Map<ESPDeviceBase, ESPDeviceBaseDTO>(hardwareEntity);
        }

        public async Task<List<ESPDeviceBaseDTO>> UpdatePins()
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

            return Mapper.Map<List<ESPDeviceBase>, List<ESPDeviceBaseDTO>>(entities);
        }

        public async Task<ESPDeviceBaseDTO> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract)
        {
            var data = await _espDeviceRepository.GetDeviceInApplication(contract.ChipId, contract.FlashChipId, contract.MacAddress);            

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }

            return Mapper.Map<ESPDeviceBase, ESPDeviceBaseDTO>(data);
        }

        #endregion Methods
    }
}