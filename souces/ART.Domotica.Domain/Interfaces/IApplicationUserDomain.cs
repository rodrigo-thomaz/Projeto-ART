namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Security.Common.Contracts;

    public interface IApplicationUserDomain
    {
        #region Methods

        Task RegisterUser(RegisterUserContract contract);

        #endregion Methods
    }
}