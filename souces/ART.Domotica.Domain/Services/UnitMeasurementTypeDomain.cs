using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Infra.CrossCutting.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Services
{
    public class UnitMeasurementTypeDomain : DomainBase, IUnitMeasurementTypeDomain
    {
        #region private readonly fields

        private readonly IUnitMeasurementTypeRepository _unitMeasurementTypeRepository;

        #endregion

        #region constructors

        public UnitMeasurementTypeDomain(IUnitMeasurementTypeRepository unitMeasurementTypeRepository)
        {
            _unitMeasurementTypeRepository = unitMeasurementTypeRepository;
        }

        #endregion

        #region public voids

        public async Task<List<UnitMeasurementType>> GetAll()
        {
            return await _unitMeasurementTypeRepository.GetAll();
        }

        #endregion
    }
}
