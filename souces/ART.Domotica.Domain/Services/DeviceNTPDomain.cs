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

    public class DeviceNTPDomain : DomainBase, IDeviceNTPDomain
    {
        #region Fields

        private readonly IDeviceNTPRepository _deviceNTPRepository;
        private readonly ITimeZoneRepository _timeZoneRepository;

        #endregion Fields

        #region Constructors

        public DeviceNTPDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceNTPRepository = new DeviceNTPRepository(context);
            _timeZoneRepository = new TimeZoneRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<DeviceNTP> SetTimeZone(Guid deviceNTPId, byte timeZoneId)
        {
            var entity = await _deviceNTPRepository.GetByKey(deviceNTPId);

            if (entity == null)
            {
                throw new Exception("DeviceNTP not found");
            }

            var timeZone = await _timeZoneRepository.GetByKey(timeZoneId);

            if (timeZone == null)
            {
                throw new Exception("Time Zone not found");
            }

            entity.TimeZoneId = timeZoneId;

            await _deviceNTPRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceNTP> SetUpdateIntervalInMilliSecond(Guid deviceNTPId, int updateIntervalInMilliSecond)
        {
            var entity = await _deviceNTPRepository.GetByKey(deviceNTPId);

            if (entity == null)
            {
                throw new Exception("DeviceNTP not found");
            }

            entity.UpdateIntervalInMilliSecond = updateIntervalInMilliSecond;

            await _deviceNTPRepository.Update(entity);

            return entity;
        }

        #endregion Methods
    }
}