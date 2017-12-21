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

    public class DeviceMQDomain : DomainBase, IDeviceMQDomain
    {
        #region Fields

        private readonly IDeviceMQRepository _deviceMQRepository;

        #endregion Fields

        #region Constructors

        public DeviceMQDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceMQRepository = new DeviceMQRepository(context);
        }

        #endregion Constructors

        #region Methods
        
        public async Task<DeviceMQ> GetByKey(Guid deviceMQId, DeviceDatasheetEnum deviceDatasheetId)
        {
            var data = await _deviceMQRepository.GetByKey(deviceMQId, deviceDatasheetId);

            if (data == null)
            {
                throw new Exception("DeviceMQ not found");
            }

            return data;
        }

        #endregion Methods
    }
}