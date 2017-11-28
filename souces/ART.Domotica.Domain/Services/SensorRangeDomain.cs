namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SensorRangeDomain : DomainBase, ISensorRangeDomain
    {
        #region Fields

        private readonly ISensorRangeRepository _sensorRangeRepository;

        #endregion Fields

        #region Constructors

        public SensorRangeDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorRangeRepository = new SensorRangeRepository(context);
        }

        #endregion Constructors

        public async Task<List<SensorRange>> GetAll()
        {
            return await _sensorRangeRepository.GetAll();
        }
    }
}