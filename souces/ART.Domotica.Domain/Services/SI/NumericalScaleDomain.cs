using System.Threading.Tasks;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Domotica.Domain.Interfaces.SI;

namespace ART.Domotica.Domain.Services.SI
{
    public class NumericalScaleDomain : DomainBase, INumericalScaleDomain
    {
        #region private readonly fields

        private readonly INumericalScaleRepository _numericalScaleRepository;

        #endregion

        #region constructors

        public NumericalScaleDomain(INumericalScaleRepository numericalScaleRepository)
        {
            _numericalScaleRepository = numericalScaleRepository;
        }

        #endregion

        #region public voids

        public async Task<List<NumericalScale>> GetAll()
        {
            return await _numericalScaleRepository.GetAll();
        }

        #endregion
    }
}
