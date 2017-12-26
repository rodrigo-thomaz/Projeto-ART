namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using ART.Domotica.Repository;
    using Autofac;
    using ART.Domotica.Repository.Repositories;
    using System.Threading.Tasks;
    using System;

    public class DeviceBinaryDomain : DomainBase, IDeviceBinaryDomain
    {
        #region Fields

        private readonly IDeviceBinaryRepository _deviceBinaryRepository;
        private readonly IDeviceDatasheetBinaryRepository _deviceDatasheetBinaryRepository;
        private readonly IDeviceDatasheetBinaryBufferRepository _deviceDatasheetBinaryBufferRepository;

        #endregion Fields

        #region Constructors

        public DeviceBinaryDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _deviceBinaryRepository = new DeviceBinaryRepository(context);
            _deviceDatasheetBinaryRepository = new DeviceDatasheetBinaryRepository(context);
            _deviceDatasheetBinaryBufferRepository = new DeviceDatasheetBinaryBufferRepository(context);
        }

        #endregion Constructors

        public async Task<byte[]> CheckForUpdates(string stationMacAddress, string softAPMacAddress)
        {
            var deviceBinary = await _deviceBinaryRepository.GetByDeviceMacAdresses(stationMacAddress, softAPMacAddress);

            if (deviceBinary == null)
            {
                throw new Exception("Device not found");
            }

            var lastVersionOfDeviceDatasheetBinary = await _deviceDatasheetBinaryRepository.GetLastVersioByDatasheetKey(deviceBinary.DeviceDatasheetId);

            if (lastVersionOfDeviceDatasheetBinary == null)
            {
                throw new Exception("Last Version Of DeviceDatasheetBinary not found");
            }

            byte[] result = null;

            if(deviceBinary.DeviceDatasheetBinaryId != lastVersionOfDeviceDatasheetBinary.Id)
            {                
                deviceBinary.DeviceDatasheetBinaryId = lastVersionOfDeviceDatasheetBinary.Id;
                await _deviceBinaryRepository.Update(deviceBinary);
                var deviceDatasheetBinaryBuffer = await _deviceDatasheetBinaryBufferRepository.GetByKey(deviceBinary.DeviceDatasheetBinaryId, deviceBinary.DeviceDatasheetId);
                result = deviceDatasheetBinaryBuffer.Buffer;
            }

            return result;;
        }
    }
}