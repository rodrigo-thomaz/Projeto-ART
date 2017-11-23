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
    using Autofac;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Repositories;

    public class ESPDeviceDomain : DomainBase, IESPDeviceDomain
    {
        #region Fields

        private readonly IESPDeviceRepository _espDeviceRepository;
        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IDeviceBrokerSettingRepository _deviceBrokerSettingRepository;
        private readonly IDeviceNTPSettingRepository _deviceNTPSettingRepository;

        #endregion Fields

        #region Constructors

        public ESPDeviceDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _espDeviceRepository = new ESPDeviceRepository(context);
            _dsFamilyTempSensorRepository = new DSFamilyTempSensorRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
            _deviceBrokerSettingRepository = new DeviceBrokerSettingRepository(context);
            _deviceNTPSettingRepository = new DeviceNTPSettingRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ESPDevice>> GetAll()
        {
            return await _espDeviceRepository.GetAll();
        }

        public async Task<List<ESPDevice>> GetListInApplication(AuthenticatedMessageContract message)
        {
            var applicationUser = await _applicationUserRepository.GetById(message.ApplicationUserId);
            return await _espDeviceRepository.GetListInApplication(applicationUser.ApplicationId);
        }

        public async Task<ESPDevice> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message)
        {
            var data = await _espDeviceRepository.GetByPin(message.Contract.Pin);

            if (data == null)
            {
                throw new Exception("Pin not found");
            }

            return data;
        }

        public async Task<ESPDevice> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message)
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

            await _deviceInApplicationRepository.Insert(new DeviceInApplication
            {
                ApplicationId = applicationUserEntity.ApplicationId,
                DeviceBaseId = hardwareEntity.Id,
                CreateByApplicationUserId = applicationUserEntity.Id,
                CreateDate = DateTime.Now.ToUniversalTime(),
            });

            return hardwareEntity;
        }

        public async Task<ESPDevice> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message)
        {
            var deviceInApplicationEntity = await _deviceInApplicationRepository.GetById(message.Contract.DeviceInApplicationId);
            
            if (deviceInApplicationEntity == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            await _deviceInApplicationRepository.Delete(deviceInApplicationEntity);

            var hardwareEntity = await _espDeviceRepository.GetById(deviceInApplicationEntity.DeviceBaseId);

            //Load Broker Setting
            await _deviceBrokerSettingRepository.GetById(deviceInApplicationEntity.DeviceBaseId);

            return hardwareEntity;
        }

        public async Task<List<ESPDevice>> UpdatePins()
        {
            var existingPins = await _espDeviceRepository.GetExistingPins();
            var entities = await _espDeviceRepository.GetListNotInApplication();

            foreach (var item in entities)
            {
                var pin = RandonHelper.RandomString(4);
                while (existingPins.Contains(pin))
                {
                    pin = RandonHelper.RandomString(4);
                }
                item.Pin = pin;
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _espDeviceRepository.Update(entities);
                scope.Complete();
            }

            return entities;
        }

        public async Task<ESPDevice> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract)
        {
            var data = await _espDeviceRepository.GetDeviceInApplication(contract.ChipId, contract.FlashChipId, contract.MacAddress);            

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }

            return data;
        }

        public async Task<DeviceBrokerSetting> GetDeviceBrokerSetting(Guid deviceId)
        {
            var data = await _deviceBrokerSettingRepository.GetById(deviceId);

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }

            return data;
        }

        public async Task<ApplicationBrokerSetting> GetApplicationBrokerSetting(Guid deviceId)
        {
            var data = await _espDeviceRepository.GetApplicationBrokerSetting(deviceId);

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }

            return data;
        }

        public async Task<ESPDevice> SetTimeOffsetInSecond(Guid deviceId, int timeOffsetInSecond)
        {
            var entity = await _deviceNTPSettingRepository.GetById(deviceId);

            if (entity == null)
            {
                throw new Exception("ESP Device not found");
            }

            entity.TimeOffsetInSecond = timeOffsetInSecond;

            await _deviceNTPSettingRepository.Update(entity);

            return await _espDeviceRepository.GetById(deviceId);
        }

        public async Task<ESPDevice> SetUpdateIntervalInMilliSecond(Guid deviceId, int updateIntervalInMilliSecond)
        {
            var entity = await _deviceNTPSettingRepository.GetById(deviceId);

            if (entity == null)
            {
                throw new Exception("ESP Device not found");
            }

            entity.UpdateIntervalInMilliSecond = updateIntervalInMilliSecond;

            await _deviceNTPSettingRepository.Update(entity);

            return await _espDeviceRepository.GetById(deviceId);
        }

        #endregion Methods
    }
}