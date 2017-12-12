using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using System;
using ART.Domotica.Repository.Repositories;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Domotica.Repository.Repositories.SI;
using ART.Domotica.Enums;

namespace ART.Domotica.Domain.Services
{
    public class SensorDomain : DomainBase, ISensorDomain
    {
        #region private readonly fields

        private readonly ISensorRepository _sensorRepository;
        private readonly ISensorTriggerRepository _sensorTriggerRepository;
        private readonly IUnitMeasurementRepository _unitMeasurementRepository;
        private readonly IHardwareInApplicationRepository _hardwareInApplicationRepository;

        #endregion

        #region constructors

        public SensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorRepository = new SensorRepository(context);
            _sensorTriggerRepository = new SensorTriggerRepository(context);
            _unitMeasurementRepository = new UnitMeasurementRepository(context);
            _hardwareInApplicationRepository = new HardwareInApplicationRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<Sensor>> GetAllByApplicationId(Guid applicationId)
        {
            return await _sensorRepository.GetAllByApplicationId(applicationId);
        }        

        public async Task<SensorInDevice> GetDeviceFromSensor(Guid sensorId)
        {
            var data = await _sensorRepository.GetDeviceFromSensor(sensorId);

            if (data == null)
            {
                throw new Exception("Sensor not found");
            }

            return data;
        }

        public async Task<Sensor> GetByKey(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
        {
            var data = await _sensorRepository.GetByKey(sensorId, sensorDatasheetId, sensorTypeId);

            if (data == null)
            {
                throw new Exception("Sensor not found");
            }

            return data;
        }       

        public async Task<List<Sensor>> GetAllByHardwareInApplicationId(Guid applicationId, Guid deviceId)
        {
            var hardwareInApplication = await _hardwareInApplicationRepository.GetByKey(applicationId, deviceId);

            if (hardwareInApplication == null)
            {
                throw new Exception("HardwareInApplication not found");
            }

            return await _sensorRepository.GetAllByDeviceId(hardwareInApplication.HardwareId);
        }

        public async Task<Sensor> SetLabel(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, string label)
        {
            var entity = await _sensorRepository.GetByKey(sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            entity.Label = label;

            await _sensorRepository.Update(entity);

            return entity;
        }

        #endregion
    }
}
