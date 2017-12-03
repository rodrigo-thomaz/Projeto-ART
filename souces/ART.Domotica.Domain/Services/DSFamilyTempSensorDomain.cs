using System.Threading.Tasks;
using System;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Repository.Repositories;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Domotica.Repository.Repositories.SI;
using ART.Domotica.Enums.SI;

namespace ART.Domotica.Domain.Services
{
    public class DSFamilyTempSensorDomain : DomainBase, IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IDSFamilyTempSensorResolutionRepository _dsFamilyTempSensorResolutionRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;
        private readonly IUnitMeasurementRepository _unitMeasurementRepository;
        private readonly ISensorTriggerRepository _sensorTriggerRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _dsFamilyTempSensorRepository = new DSFamilyTempSensorRepository(context);
            _dsFamilyTempSensorResolutionRepository = new DSFamilyTempSensorResolutionRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
            _unitMeasurementRepository = new UnitMeasurementRepository(context);
            _sensorTriggerRepository = new SensorTriggerRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetById(deviceInApplicationId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _dsFamilyTempSensorRepository.GetAllByDeviceId(deviceInApplication.DeviceBaseId);            
        }
        
        public async Task<List<DSFamilyTempSensorResolution>> GetAllResolutions()
        {
            return await _dsFamilyTempSensorResolutionRepository.GetAll();
        }

        public async Task<DSFamilyTempSensor> SetUnitMeasurement(Guid dsFamilyTempSensorId, UnitMeasurementEnum unitMeasurementId)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if(dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var unitMeasurementEntity = await _unitMeasurementRepository.GetByKey(unitMeasurementId, UnitMeasurementTypeEnum.Temperature);

            if (unitMeasurementEntity == null)
            {
                throw new Exception("UnitMeasurement not found");
            }
            
            dsFamilyTempSensorEntity.UnitMeasurementId = unitMeasurementEntity.Id;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);

            return dsFamilyTempSensorEntity;
        }

        public async Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var dsFamilyTempSensorResolutionEntity = await _dsFamilyTempSensorResolutionRepository.GetById(dsFamilyTempSensorResolutionId);

            if (dsFamilyTempSensorResolutionEntity == null)
            {
                throw new Exception("DSFamilyTempSensorResolution not found");
            }
            
            dsFamilyTempSensorEntity.DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolutionEntity.Id;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);

            return dsFamilyTempSensorEntity;
        }
        
        #endregion
    }
}
 