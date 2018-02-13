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

    public class DeviceDisplayDomain : DomainBase, IDeviceDisplayDomain
    {
        #region Fields

        private readonly IDeviceDisplayRepository _deviceDisplayRepository;

        #endregion Fields

        #region Constructors

        public DeviceDisplayDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceDisplayRepository = new DeviceDisplayRepository(context);
        }

        #endregion Constructors

        #region Methods        

        public async Task<DeviceDisplay> SetEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value)
        {
            var entity = await _deviceDisplayRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId);

            if (entity == null)
            {
                throw new Exception("DeviceDisplay not found");
            }

            entity.Enabled = value;

            await _deviceDisplayRepository.Update(entity);

            return entity;
        } 

        #endregion Methods
    }
}