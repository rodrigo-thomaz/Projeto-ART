using System.Threading.Tasks;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Domotica.Domain.Interfaces.SI;

namespace ART.Domotica.Domain.Services.SI
{
    public class NumericalScaleTypeDomain : DomainBase, INumericalScaleTypeDomain
    {
        #region private readonly fields

        private readonly INumericalScaleTypeRepository _numericalScaleTypeRepository;

        #endregion

        #region constructors

        public NumericalScaleTypeDomain(INumericalScaleTypeRepository numericalScaleTypeRepository)
        {
            _numericalScaleTypeRepository = numericalScaleTypeRepository;
        }

        #endregion

        #region public voids

        public async Task<List<NumericalScaleType>> GetAll()
        {
            return await _numericalScaleTypeRepository.GetAll();
        }

        #endregion
    }
}
