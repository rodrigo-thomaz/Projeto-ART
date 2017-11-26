namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using System;

    public interface IApplicationDomain
    {
        #region Methods

        Task<Application> GetById(Guid applicationId);

        #endregion Methods
    }
}