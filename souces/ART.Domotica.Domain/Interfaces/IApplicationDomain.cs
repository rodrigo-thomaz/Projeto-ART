namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IApplicationDomain
    {
        #region Methods

        Task<Application> GetByKey(Guid applicationId);

        #endregion Methods
    }
}