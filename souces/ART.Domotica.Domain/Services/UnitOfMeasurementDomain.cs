using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class UnitOfMeasurementDomain : DomainBase, IUnitOfMeasurementDomain
    {
        #region private readonly fields

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;

        #endregion

        #region constructors

        public UnitOfMeasurementDomain(IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion

        #region public voids

        public async Task<List<UnitOfMeasurement>> GetAll()
        {
            return await _unitOfMeasurementRepository.GetAll();
        }

        #endregion
    }
}
