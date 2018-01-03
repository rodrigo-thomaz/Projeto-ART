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

        public async Task<DeviceDebug> SetActive(Guid deviceDebugId, DeviceDatasheetEnum deviceDatasheetId, bool active)
        {
            var entity = await _deviceDebugRepository.GetByKey(deviceDebugId, deviceDatasheetId);

            if (entity == null)
            {
                throw new Exception("DeviceDebug not found");
            }

            entity.Active = active;

            await _deviceDebugRepository.Update(entity);

            return entity;
        }

        #endregion Methods
    }
}