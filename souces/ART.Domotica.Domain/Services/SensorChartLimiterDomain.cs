namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;

    public class SensorChartLimiterDomain : DomainBase, ISensorRangeDomain
    {
        #region Fields

        private readonly ISensorChartLimiterRepository _sensorChartLimiterRepository;

        #endregion Fields

        #region Constructors

        public SensorChartLimiterDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorChartLimiterRepository = new SensorChartLimiterRepository(context);
        }

        #endregion Constructors
    }
}