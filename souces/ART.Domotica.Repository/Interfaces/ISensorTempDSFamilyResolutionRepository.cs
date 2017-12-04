namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorTempDSFamilyResolutionRepository : IRepository<ARTDbContext, SensorTempDSFamilyResolution, byte>
    {
        #region Methods

        Task<List<SensorTempDSFamilyResolution>> GetAll();

        #endregion Methods
    }
}