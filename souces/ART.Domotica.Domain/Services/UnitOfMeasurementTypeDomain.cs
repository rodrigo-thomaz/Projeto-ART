using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Services
{
    public class UnitOfMeasurementTypeDomain : DomainBase, IUnitOfMeasurementTypeDomain
    {
        #region private readonly fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion

        #region constructors

        public UnitOfMeasurementTypeDomain(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion

        #region public voids

        public async Task<List<UnitOfMeasurementType>> GetAll()
        {
            return await _unitOfMeasurementTypeRepository.GetAll();
        }

        #endregion
    }
}
