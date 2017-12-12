namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Entities;
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
        private readonly IHardwareInApplicationRepository _hardwareInApplicationRepository;
        private readonly ISensorInApplicationRepository _sensorInApplicationRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISensorRepository _sensorRepository;

        #endregion Fields

        #region Constructors

        public ESPDeviceDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _espDeviceRepository = new ESPDeviceRepository(context);
            _applicationRepository = new ApplicationRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
            _hardwareInApplicationRepository = new HardwareInApplicationRepository(context);
            _sensorInApplicationRepository = new SensorInApplicationRepository(context);
            _sensorRepository = new SensorRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ESPDevice>> GetAll()
        {
            return await _espDeviceRepository.GetAll();
        }

        public async Task<List<ESPDevice>> GetAllByApplicationId(Guid applicationId)
        {
            return await _espDeviceRepository.GetAllByApplicationId(applicationId);
        }

        public async Task<ESPDevice> GetByPin(string pin)
        {
            var data = await _espDeviceRepository.GetByPin(pin);

            if (data == null)
            {
                throw new Exception("Pin not found");
            }

            return data;
        }

        public async Task<ESPDevice> InsertInApplication(Guid applicationId, Guid createByApplicationUserId, string pin)
        {
            var applicationEntity = await _applicationRepository.GetByKey(applicationId);

            if (applicationEntity == null)
            {
                throw new Exception("Application not found");
            }

            var deviceEntity = await _espDeviceRepository.GetByPin(pin);

            if (deviceEntity == null)
            {
                throw new Exception("Pin not found");
            }

            var applicationUserEntity = await _applicationUserRepository.GetByKey(createByApplicationUserId);

            if (applicationUserEntity == null)
            {
                throw new Exception("ApplicationUser not found");
            }

            await _hardwareInApplicationRepository.Insert(new HardwareInApplication
            {
                ApplicationId = applicationEntity.Id,
                HardwareId = deviceEntity.Id,
                CreateByApplicationUserId = applicationUserEntity.Id,
                CreateDate = DateTime.Now.ToUniversalTime(),
            });

            var sensors = await _sensorRepository.GetAllByDeviceId(deviceEntity.Id);

            var sensorsInApplication = new List<SensorInApplication>();

            foreach (var item in sensors)
            {
                sensorsInApplication.Add(new SensorInApplication
                {
                    ApplicationId = applicationEntity.Id,
                    SensorId = item.Id,
                    SensorDatasheetId = item.SensorDatasheetId,
                    SensorTypeId = item.SensorTypeId,
                    CreateByApplicationUserId = applicationUserEntity.Id,
                    CreateDate = DateTime.Now.ToUniversalTime(),
                });
            }

            await _sensorInApplicationRepository.Insert(sensorsInApplication);

            return deviceEntity;
        }

        public async Task<ESPDevice> DeleteFromApplication(Guid applicationId, Guid deviceId)
        {
            // Device 

            HardwareInApplication hardwareInApplicationEntity = await _hardwareInApplicationRepository.GetByKey(applicationId, deviceId);
            
            if (hardwareInApplicationEntity == null)
            {
                throw new Exception("HardwareInApplication not found");
            }

            await _hardwareInApplicationRepository.Delete(hardwareInApplicationEntity);

            // Sensors

            var sensorsInApplication = await _sensorRepository.GetSensorsInApplicationByDeviceId(applicationId, deviceId);           

            await _sensorInApplicationRepository.Delete(sensorsInApplication);

            var deviceEntity = await _espDeviceRepository.GetFullByKey(hardwareInApplicationEntity.HardwareId);

            return deviceEntity;
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

        public async Task<ESPDevice> GetConfigurations(int chipId, int flashChipId, string macAddress)
        {
            var data = await _espDeviceRepository.GetHardwareInApplication(chipId, flashChipId, macAddress);            

            if (data == null)
            {
                throw new Exception("ESP Device not found");
            }

            return data;
        }

        #endregion Methods
    }
}