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

    public class DeviceSensorsDomain : DomainBase, IDeviceSensorsDomain
    {
        #region Fields

        private readonly IDeviceSensorsRepository _deviceSensorsRepository;

        #endregion Fields

        #region Constructors

        public DeviceSensorsDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _deviceSensorsRepository = new DeviceSensorsRepository(context);
        }

        #endregion Constructors

        public async Task<DeviceSensors> SetPublishIntervalInSeconds(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId, int publishIntervalInSeconds)
        {
            var entity = await _deviceSensorsRepository.GetByKey(deviceSensorsId, deviceDatasheetId);

            if (entity == null)
            {
                throw new Exception("DeviceSensors not found");
            }

            entity.PublishIntervalInSeconds = publishIntervalInSeconds;

            await _deviceSensorsRepository.Update(entity);

            return entity;
        }
    }
}