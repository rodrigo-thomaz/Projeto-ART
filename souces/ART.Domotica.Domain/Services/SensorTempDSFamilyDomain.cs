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

namespace ART.Domotica.Domain.Services
{
    public class SensorTempDSFamilyDomain : DomainBase, ISensorTempDSFamilyDomain
    {
        #region private readonly fields

        private readonly ISensorTempDSFamilyRepository _sensorTempDSFamilyRepository;
        private readonly ISensorTempDSFamilyResolutionRepository _sensorTempDSFamilyResolutionRepository;
        
        #endregion

        #region constructors

        public SensorTempDSFamilyDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorTempDSFamilyRepository = new SensorTempDSFamilyRepository(context);
            _sensorTempDSFamilyResolutionRepository = new SensorTempDSFamilyResolutionRepository(context);            
        }

        #endregion

        #region public voids
                
        public async Task<List<SensorTempDSFamilyResolution>> GetAllResolutions()
        {
            return await _sensorTempDSFamilyResolutionRepository.GetAll();
        }
        
        public async Task<SensorTempDSFamily> SetResolution(Guid sensorTempDSFamilyId, byte sensorTempDSFamilyResolutionId)
        {
            var sensorTempDSFamilyEntity = await _sensorTempDSFamilyRepository.GetById(sensorTempDSFamilyId);

            if (sensorTempDSFamilyEntity == null)
            {
                throw new Exception("SensorTempDSFamily not found");
            }

            var sensorTempDSFamilyResolutionEntity = await _sensorTempDSFamilyResolutionRepository.GetById(sensorTempDSFamilyResolutionId);

            if (sensorTempDSFamilyResolutionEntity == null)
            {
                throw new Exception("SensorTempDSFamilyResolution not found");
            }
            
            sensorTempDSFamilyEntity.SensorTempDSFamilyResolutionId = sensorTempDSFamilyResolutionEntity.Id;

            await _sensorTempDSFamilyRepository.Update(sensorTempDSFamilyEntity);

            return sensorTempDSFamilyEntity;
        }
        
        #endregion
    }
}
 