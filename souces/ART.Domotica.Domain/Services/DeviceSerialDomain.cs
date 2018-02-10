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

    public class DeviceSerialDomain : DomainBase, IDeviceSerialDomain
    {
        #region Fields

        private readonly IDeviceSerialRepository _deviceSerialRepository;

        #endregion Fields

        #region Constructors

        public DeviceSerialDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceSerialRepository = new DeviceSerialRepository(context);
        }

        #endregion Constructors

        #region Methods
        
        public async Task<DeviceSerial> GetByKey(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId)
        {
            var data = await _deviceSerialRepository.GetByKey(deviceSerialId, deviceId, deviceDatasheetId);

            if (data == null)
            {
                throw new Exception("DeviceSerial not found");
            }

            return data;
        }

        #endregion Methods
    }
}