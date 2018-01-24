namespace ART.Domotica.Domain.Services
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Repositories;
    using ART.Domotica.Repository;
    using Autofac;
    using System.Collections.Generic;

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

        public async Task<DeviceSensors> SetPublishIntervalInMilliSeconds(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId, int publishIntervalInMilliSeconds)
        {
            var entity = await _deviceSensorsRepository.GetByKey(deviceSensorsId, deviceDatasheetId);

            if (entity == null)
            {
                throw new Exception("DeviceSensors not found");
            }

            entity.PublishIntervalInMilliSeconds = publishIntervalInMilliSeconds;

            await _deviceSensorsRepository.Update(entity);

            return entity;
        }

        public async Task<List<SensorInDevice>> GetAllByDeviceInApplicationId(Guid applicationId, Guid deviceId, DeviceDatasheetEnum deviceDatasheetId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetByKey(applicationId, deviceId, deviceDatasheetId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _deviceSensorsRepository.GetAllByDeviceId(deviceInApplication.DeviceId);
        }
    }
}