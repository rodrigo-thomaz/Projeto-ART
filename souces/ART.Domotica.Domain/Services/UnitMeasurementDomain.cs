using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class UnitMeasurementDomain : DomainBase, IUnitMeasurementDomain
    {
        #region private readonly fields

        private readonly IUnitMeasurementRepository _unitMeasurementRepository;

        #endregion

        #region constructors

        public UnitMeasurementDomain(IUnitMeasurementRepository unitMeasurementRepository)
        {
            _unitMeasurementRepository = unitMeasurementRepository;
        }

        #endregion

        #region public voids

        public async Task<List<UnitMeasurement>> GetAll()
        {
            return await _unitMeasurementRepository.GetAll();
        }

        #endregion
    }
}
