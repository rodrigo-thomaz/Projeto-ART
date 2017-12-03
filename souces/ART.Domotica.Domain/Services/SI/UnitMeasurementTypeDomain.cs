using ART.Domotica.Domain.Interfaces.SI;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Infra.CrossCutting.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Repository.Repositories.SI;

namespace ART.Domotica.Domain.Services.SI
{
    public class UnitMeasurementTypeDomain : DomainBase, IUnitMeasurementTypeDomain
    {
        #region private readonly fields

        private readonly IUnitMeasurementTypeRepository _unitMeasurementTypeRepository;

        #endregion

        #region constructors

        public UnitMeasurementTypeDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _unitMeasurementTypeRepository = new UnitMeasurementTypeRepository(context);
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
