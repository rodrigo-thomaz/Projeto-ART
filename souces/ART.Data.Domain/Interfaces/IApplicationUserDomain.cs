namespace ART.Data.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;

    public interface IApplicationUserDomain
    {
        #region Methods

        Task RegisterUser(ApplicationUser applicationUser);

        #endregion Methods
    }
}