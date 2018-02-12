namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Entities;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using Autofac;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Repositories;
    using ART.Domotica.Enums;

    public class DeviceDebugDomain : DomainBase, IDeviceDebugDomain
    {
        #region Fields

        private readonly IDeviceDebugRepository _deviceDebugRepository;

        #endregion Fields

        #region Constructors

        public DeviceDebugDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceDebugRepository = new DeviceDebugRepository(context);
        }

        #endregion Constructors

        #region Methods        

        public async Task<DeviceDebug> SetRemoteEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.RemoteEnabled = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }        

        public async Task<DeviceDebug> SetSerialEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.SerialEnabled = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceDebug> SetResetCmdEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.ResetCmdEnabled = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }        

        public async Task<DeviceDebug> SetShowColors(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.ShowColors = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceDebug> SetShowDebugLevel(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.ShowDebugLevel = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceDebug> SetShowProfiler(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.ShowProfiler = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceDebug> SetShowTime(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.ShowTime = value;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }

        #endregion Methods
    }
}