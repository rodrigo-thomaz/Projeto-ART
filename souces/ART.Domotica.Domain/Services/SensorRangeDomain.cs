namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;

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
    }
}