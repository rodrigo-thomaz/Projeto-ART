using System.Threading.Tasks;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Domotica.Domain.Interfaces.SI;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Repository.Repositories.SI;

namespace ART.Domotica.Domain.Services.SI
{
    public class NumericalScaleDomain : DomainBase, INumericalScaleDomain
    {
        #region private readonly fields

        private readonly INumericalScaleRepository _numericalScaleRepository;

        #endregion

        #region constructors

        public NumericalScaleDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _numericalScaleRepository = new NumericalScaleRepository(context);
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
