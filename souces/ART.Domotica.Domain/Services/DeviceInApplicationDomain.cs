namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Entities;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using Autofac;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Repositories;

    public class DeviceInApplicationDomain : DomainBase, IDeviceInApplicationDomain
    {
        #region Fields

        private readonly IESPDeviceRepository _espDeviceRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;
        private readonly ISensorInApplicationRepository _sensorInApplicationRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISensorRepository _sensorRepository;
        private readonly IDeviceSensorsRepository _deviceSensorsRepository;

        #endregion Fields

        #region Constructors

        public DeviceInApplicationDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _espDeviceRepository = new ESPDeviceRepository(context);
            _applicationRepository = new ApplicationRepository(context);
            _applicationUserRepository = new ApplicationUserRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
            _sensorInApplicationRepository = new SensorInApplicationRepository(context);
            _sensorRepository = new SensorRepository(context);
            _deviceSensorsRepository = new DeviceSensorsRepository(context);
        }

        #endregion Constructors

        #region Methods       

        public async Task<ESPDevice> Insert(Guid applicationId, Guid createByApplicationUserId, string pin)
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

            var deviceInApplication = new DeviceInApplication
            {
                ApplicationId = applicationEntity.Id,
                DeviceId = deviceEntity.Id,
                DeviceDatasheetId = deviceEntity.DeviceDatasheetId,
                CreateByApplicationUserId = applicationUserEntity.Id,
                CreateDate = DateTime.Now.ToUniversalTime(),

            };

            await _deviceInApplicationRepository.Insert(deviceInApplication);

            var deviceSensors = await _deviceSensorsRepository.GetFullByDeviceId(deviceEntity.Id);

            var sensorsInApplication = new List<SensorInApplication>();

            foreach (var item in deviceSensors.SensorInDevice)
            {
                sensorsInApplication.Add(new SensorInApplication
                {
                    ApplicationId = applicationEntity.Id,
                    SensorId = item.Sensor.Id,
                    SensorDatasheetId = item.SensorDatasheetId,
                    SensorTypeId = item.SensorTypeId,
                    CreateByApplicationUserId = applicationUserEntity.Id,
                    CreateDate = DateTime.Now.ToUniversalTime(),
                });
            }

            await _sensorInApplicationRepository.Insert(sensorsInApplication);

            return deviceEntity;
        }

        public async Task<DeviceBase> Remove(Guid applicationId, Guid deviceId, Guid deviceDatasheetId)
        {
            // Device 

            DeviceInApplication deviceInApplicationEntity = await _deviceInApplicationRepository.GetByKey(applicationId, deviceId, deviceDatasheetId);
            
            if (deviceInApplicationEntity == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            await _deviceInApplicationRepository.Delete(deviceInApplicationEntity);

            // Sensors

            var sensorsInApplication = await _sensorRepository.GetSensorsInApplicationByDeviceId(applicationId, deviceId);           

            await _sensorInApplicationRepository.Delete(sensorsInApplication);

            var deviceEntity = await _espDeviceRepository.GetFullByKey(deviceInApplicationEntity.DeviceId, deviceInApplicationEntity.DeviceDatasheetId);

            return deviceEntity;
        }

        #endregion Methods
    }
}