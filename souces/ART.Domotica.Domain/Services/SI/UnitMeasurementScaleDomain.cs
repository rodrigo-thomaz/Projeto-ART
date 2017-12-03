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
    public class UnitMeasurementScaleDomain : DomainBase, IUnitMeasurementScaleDomain
    {
        #region private readonly fields

        private readonly IUnitMeasurementScaleRepository _unitMeasurementScaleRepository;

        #endregion

        #region constructors

        public UnitMeasurementScaleDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _unitMeasurementScaleRepository = new UnitMeasurementScaleRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<UnitMeasurementScale>> GetAll()
        {
            return await _unitMeasurementScaleRepository.GetAll();
        }

        #endregion
    }
}
