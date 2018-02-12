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

    public class DeviceSensorsDomain : DomainBase, IDeviceSensorsDomain
    {
        #region Fields

        private readonly IDeviceSensorsRepository _deviceSensorsRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;

        #endregion Fields

        #region Constructors

        public DeviceSensorsDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _deviceSensorsRepository = new DeviceSensorsRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
        }

        #endregion Constructors

        public async Task<DeviceSensors> SetReadIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long readIntervalInMilliSeconds)
        {
            var entity = await _deviceSensorsRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceSensors not found");
            }

            entity.ReadIntervalInMilliSeconds = readIntervalInMilliSeconds;

            await _deviceSensorsRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceSensors> SetPublishIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long publishIntervalInMilliSeconds)
        {
            var entity = await _deviceSensorsRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceSensors not found");
            }

            entity.PublishIntervalInMilliSeconds = publishIntervalInMilliSeconds;

            await _deviceSensorsRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceSensors> GetFullByDeviceInApplicationId(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetByKey(applicationId, deviceTypeId, deviceDatasheetId, deviceId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _deviceSensorsRepository.GetFullByDeviceId(deviceInApplication.DeviceId);
        }
    }
}