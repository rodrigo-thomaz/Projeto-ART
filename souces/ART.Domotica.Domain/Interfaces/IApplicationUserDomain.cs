namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IApplicationUserDomain
    {
        #region Methods

        Task<ApplicationUser> GetById(Guid applicationUserId);

        Task RegisterUser(ApplicationUser entity);

        #endregion Methods
    }
}