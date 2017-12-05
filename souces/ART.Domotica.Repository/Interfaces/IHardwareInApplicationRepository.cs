namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IHardwareInApplicationRepository : IRepository<ARTDbContext, HardwareInApplication>
    {
        #region Methods

        Task<HardwareInApplication> GetByKey(Guid applicationId, Guid deviceId);

        #endregion Methods
    }
}