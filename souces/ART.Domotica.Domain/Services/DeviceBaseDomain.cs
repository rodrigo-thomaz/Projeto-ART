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

    public class DeviceBaseDomain : DomainBase, IDeviceBaseDomain
    {
        #region Fields

        private readonly IDeviceBaseRepository _deviceBaseRepository;

        #endregion Fields

        #region Constructors

        public DeviceBaseDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _deviceBaseRepository = new DeviceBaseRepository(context);
        }

        #endregion Constructors

        #region Methods
        
        public async Task<DeviceBase> SetLabel(Guid deviceId, Guid deviceDatasheetId, string label)
        {
            var entity = await _deviceBaseRepository.GetByKey(deviceId, deviceDatasheetId);

            if (entity == null)
            {
                throw new Exception("Device not found");
            }

            entity.Label = label;

            await _deviceBaseRepository.Update(entity);

            return entity;
        }

        #endregion Methods
    }
}