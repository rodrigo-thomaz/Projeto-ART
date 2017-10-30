namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using ART.Infra.CrossCutting.Utils;
    using System.Collections.Generic;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Contract;

    public class HardwareDomain : DomainBase, IHardwareDomain
    {
        #region Fields

        private readonly IHardwareRepository _hardwareRepository;

        #endregion Fields

        #region Constructors

        public HardwareDomain(IHardwareRepository hardwareRepository)
        {        
            _hardwareRepository = hardwareRepository;
        }
        
        #endregion Constructors

        #region Methods

        

        #endregion Methods
    }
}