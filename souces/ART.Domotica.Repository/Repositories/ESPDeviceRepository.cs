namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ESPDeviceRepository : RepositoryBase<ARTDbContext, ESPDevice, Guid>, IESPDeviceRepository
    {
        #region Constructors

        public ESPDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ESPDevice>> GetAll()
        {
            return await _context.ESPDevice
                .Include(x => x.DevicesInApplication)
                .ToListAsync();
        }

        public async Task<ESPDevice> GetByPin(string pin)
        {
            var data = await _context.ESPDevice
                .Include(x => x.SensorsInDevice.Select(y => y.Sensor.SensorTriggers))
                .Include(x => x.SensorsInDevice.Select(y => y.Sensor.SensorUnitMeasurementScale))
                .Include(x => x.DeviceMQ)
                .Where(x => x.Pin == pin)
                .SingleOrDefaultAsync();

            return data;
        }

        public async Task<List<string>> GetExistingPins()
        {
            var data = await _context.ESPDevice
                .Where(x => x.DevicesInApplication.Any())
                .Select(x => x.Pin)
                .ToListAsync();
            return data;
        }

        public async Task<List<ESPDevice>> GetListNotInApplication()
        {
            var data = await _context.ESPDevice
                .Include(x => x.DeviceMQ)
                .Where(x => !x.DevicesInApplication.Any())
                .ToListAsync();

            return data;
        }

        public async Task<ESPDevice> GetDeviceInApplication(int chipId, int flashChipId, string macAddress)
        {
            var data = await _context.ESPDevice
               .Include(x => x.DevicesInApplication)
               .Include(x => x.DeviceMQ)
               .Include(x => x.DeviceNTP.TimeZone)
               .Where(x => x.ChipId == chipId)
               .Where(x => x.FlashChipId == flashChipId)
               .Where(x => x.MacAddress == macAddress)               
               .SingleOrDefaultAsync();

            return data;
        }

        public async Task<List<ESPDevice>> GetListInApplication(Guid applicationId)
        {
            var data = await _context.ESPDevice
                .Include(x => x.DevicesInApplication)
                .Include(x => x.DeviceNTP)
                .Include(x => x.SensorsInDevice.Select(y => y.Sensor.SensorTriggers))
                .Include(x => x.SensorsInDevice.Select(y => y.Sensor.SensorUnitMeasurementScale))
                .Where(x => x.DevicesInApplication.Any(y => y.ApplicationId == applicationId))
                .ToListAsync();           

            return data;
        } 

        #endregion Methods
    }
}