namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IApplicationUserDomain
    {
        #region Methods

        Task RegisterUser(ApplicationUser entity);

        #endregion Methods
    }
}