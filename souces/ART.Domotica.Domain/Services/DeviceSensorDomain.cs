namespace ART.Domotica.Domain.Services
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using ART.Domotica.Repository.Repositories;
    using ART.Domotica.Repository;
    using Autofac;
    using ART.Domotica.Enums;

    public class DeviceSensorDomain : DomainBase, IDeviceSensorDomain
    {
        #region Fields

        private readonly IDeviceSensorRepository _deviceSensorRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;

        #endregion Fields

        #region Constructors

        public DeviceSensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _deviceSensorRepository = new DeviceSensorRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
        }

        #endregion Constructors

        public async Task<DeviceSensor> SetReadIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long readIntervalInMilliSeconds)
        {
            var entity = await _deviceSensorRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceSensor not found");
            }

            entity.ReadIntervalInMilliSeconds = readIntervalInMilliSeconds;

            await _deviceSensorRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceSensor> SetPublishIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long publishIntervalInMilliSeconds)
        {
            var entity = await _deviceSensorRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceSensor not found");
            }

            entity.PublishIntervalInMilliSeconds = publishIntervalInMilliSeconds;

            await _deviceSensorRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceSensor> GetFullByDeviceInApplicationId(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetByKey(applicationId, deviceTypeId, deviceDatasheetId, deviceId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _deviceSensorRepository.GetFullByDeviceId(deviceInApplication.DeviceId);
        }
    }
}