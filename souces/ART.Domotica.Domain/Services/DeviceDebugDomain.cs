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
    using System.Collections.Generic;

    public class DeviceDebugDomain : DomainBase, IDeviceDebugDomain
    {
        #region Fields

        private readonly IDeviceDebugRepository _deviceDebugRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;

        #endregion Fields

        #region Constructors

        public DeviceDebugDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceDebugRepository = new DeviceDebugRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
        }

        #endregion Constructors

        #region Methods        

        public async Task<List<DeviceDebug>> GetAllByKey(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetByKey(applicationId, deviceTypeId, deviceDatasheetId, deviceId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _deviceDebugRepository.GetAllByKey(deviceTypeId, deviceDatasheetId, deviceId);
        }

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