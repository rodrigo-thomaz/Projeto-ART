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
    public class NumericalScaleTypeDomain : DomainBase, INumericalScaleTypeDomain
    {
        #region private readonly fields

        private readonly INumericalScaleTypeRepository _numericalScaleTypeRepository;

        #endregion

        #region constructors

        public NumericalScaleTypeDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _numericalScaleTypeRepository = new NumericalScaleTypeRepository(context);
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
