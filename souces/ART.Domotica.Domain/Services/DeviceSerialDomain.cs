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

        public async Task<DeviceSerial> SetEnabled(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId, bool enabled)
        {
            var entity = await _deviceSerialRepository.GetByKey(deviceSerialId, deviceId, deviceDatasheetId);

            if (entity == null)
            {
                throw new Exception("DeviceSerial not found");
            }

            entity.Enabled = enabled;

            await _deviceSerialRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceSerial> SetPin(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId, short value, CommunicationDirection direction)
        {
            var entity = await _deviceSerialRepository.GetByKey(deviceSerialId, deviceId, deviceDatasheetId);

            if (entity == null)
            {
                throw new Exception("DeviceSerial not found");
            }

            if (direction == CommunicationDirection.Receive)
            {
                if(!entity.HasRX)
                    throw new Exception("DeviceSerial not has RX");

                if (entity.AllowPinSwapRX.HasValue && !entity.AllowPinSwapRX.Value)
                    throw new Exception("DeviceSerial not allow pin swap RX");

                entity.PinRX = value;
            }
            else if (direction == CommunicationDirection.Transmit)
            {
                if (!entity.HasTX)
                    throw new Exception("DeviceSerial not has TX");

                if (entity.AllowPinSwapTX.HasValue && !entity.AllowPinSwapTX.Value)
                    throw new Exception("DeviceSerial not allow pin swap TX");

                entity.PinTX = value;
            }

            await _deviceSerialRepository.Update(entity);

            return entity;
        }

        #endregion Methods
    }
}