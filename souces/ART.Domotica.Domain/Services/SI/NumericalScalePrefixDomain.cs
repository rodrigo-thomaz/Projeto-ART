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
    public class NumericalScalePrefixDomain : DomainBase, INumericalScalePrefixDomain
    {
        #region private readonly fields

        private readonly INumericalScalePrefixRepository _numericalScalePrefixRepository;

        #endregion

        #region constructors

        public NumericalScalePrefixDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _numericalScalePrefixRepository = new NumericalScalePrefixRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<NumericalScalePrefix>> GetAll()
        {
            return await _numericalScalePrefixRepository.GetAll();
        }

        #endregion
    }
}
