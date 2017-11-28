namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;

    public class SensorTriggerDomain : DomainBase, ISensorTriggerDomain
    {
        #region Fields

        private readonly ISensorTriggerRepository _sensorTriggerRepository;

        #endregion Fields

        #region Constructors

        public SensorTriggerDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorTriggerRepository = new SensorTriggerRepository(context);
        }

        #endregion Constructors
    }
}