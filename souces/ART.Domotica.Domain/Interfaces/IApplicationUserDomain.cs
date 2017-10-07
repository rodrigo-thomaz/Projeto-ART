namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Security.Contract;

    public interface IApplicationUserDomain
    {
        #region Methods

        Task RegisterUser(RegisterUserContract contract);

        #endregion Methods
    }
}