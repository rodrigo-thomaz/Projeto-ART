﻿using System.Threading.Tasks;
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
    public class UnitMeasurementDomain : DomainBase, IUnitMeasurementDomain
    {
        #region private readonly fields

        private readonly IUnitMeasurementRepository _unitMeasurementRepository;

        #endregion

        #region constructors

        public UnitMeasurementDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _unitMeasurementRepository = new UnitMeasurementRepository(context);
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
