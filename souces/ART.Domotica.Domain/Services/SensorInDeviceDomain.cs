namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SensorInDeviceDomain : DomainBase, ISensorInDeviceDomain
    {
        #region Fields

        private readonly ISensorInDeviceRepository _sensorInDeviceRepository;

        #endregion Fields

        #region Constructors

        public SensorInDeviceDomain(ISensorInDeviceRepository sensorInDeviceRepository)
        {
            _sensorInDeviceRepository = sensorInDeviceRepository;
        }

        #endregion Constructors

        public async Task<SensorInDevice> SetOrdination(Guid deviceSensorsId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, short ordination)
        {
            var entities = await _sensorInDeviceRepository.GetByDeviceSensorsKey(deviceSensorsId);            

            var sensorInDevice = entities
                .Where(x => x.DeviceSensorsId == deviceSensorsId)
                .Where(x => x.SensorId == sensorId)
                .Where(x => x.SensorDatasheetId == sensorDatasheetId)
                .Where(x => x.SensorTypeId == sensorTypeId)
                .FirstOrDefault();

            if(sensorInDevice == null)
            {
                throw new Exception("SensorInDevice not found");
            }

            var ordered = entities.OrderBy(x => x.Ordination).ToList();

            for (int i = 0; i < ordered.Count(); i++)
            {
                if (sensorInDevice == ordered[i])
                {

                }
            }

            foreach (var item in ordered)
            {
                if(item == sensorInDevice)
                {

                }
            }

            await _sensorInDeviceRepository.Update(entities);

            return sensorInDevice;
        }
    }
}