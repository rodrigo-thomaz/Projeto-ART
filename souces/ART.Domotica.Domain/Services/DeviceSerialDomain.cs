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

    public class DeviceSerialDomain : DomainBase, IDeviceSerialDomain
    {
        #region Fields

        private readonly IDeviceSerialRepository _deviceSerialRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;

        #endregion Fields

        #region Constructors

        public DeviceSerialDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();
                        
            _deviceSerialRepository = new DeviceSerialRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
        }

        #endregion Constructors

        #region Methods

        public async Task<DeviceSerial> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId)
        {
            var data = await _deviceSerialRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId, deviceSerialId);

            if (data == null)
            {
                throw new Exception("DeviceSerial not found");
            }

            return data;
        }

        public async Task<List<DeviceSerial>> GetAllByDeviceKey(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetByKey(applicationId, deviceTypeId, deviceDatasheetId, deviceId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _deviceSerialRepository.GetAllByDeviceKey(deviceTypeId, deviceDatasheetId, deviceId);
        }

        public async Task<DeviceSerial> SetEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId, bool enabled)
        {
            var entity = await _deviceSerialRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId, deviceSerialId);

            if (entity == null)
            {
                throw new Exception("DeviceSerial not found");
            }

            entity.Enabled = enabled;

            await _deviceSerialRepository.Update(entity);

            return entity;
        }

        public async Task<DeviceSerial> SetPin(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId, short value, CommunicationDirection direction)
        {
            var entity = await _deviceSerialRepository.GetByKey(deviceTypeId, deviceDatasheetId, deviceId, deviceSerialId);

            if (entity == null)
            {
                throw new Exception("DeviceSerial not found");
            }

            if (direction == CommunicationDirection.Receive)
            {
                if(entity.SerialMode == SerialModeEnum.TXOnly)
                    throw new Exception("DeviceSerial TXOnly mode");

                if (entity.AllowPinSwapRX.HasValue && !entity.AllowPinSwapRX.Value)
                    throw new Exception("DeviceSerial not allow pin swap RX");

                entity.PinRX = value;
            }
            else if (direction == CommunicationDirection.Transmit)
            {
                if (entity.SerialMode == SerialModeEnum.RXOnly)
                    throw new Exception("DeviceSerial RXOnly mode");

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