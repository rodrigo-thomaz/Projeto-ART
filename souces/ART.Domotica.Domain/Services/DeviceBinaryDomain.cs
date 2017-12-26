namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using ART.Domotica.Repository;
    using Autofac;
    using ART.Domotica.Repository.Repositories;

    public class DeviceBinaryDomain : DomainBase, IDeviceBinaryDomain
    {
        #region Fields

        private readonly IDeviceBinaryRepository _deviceBinaryRepository;

        #endregion Fields

        #region Constructors

        public DeviceBinaryDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _deviceBinaryRepository = new DeviceBinaryRepository(context);
        }

        #endregion Constructors

        
    }
}