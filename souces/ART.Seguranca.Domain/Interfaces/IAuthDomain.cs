namespace ART.Seguranca.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Seguranca.Repository.Entities;

    using Microsoft.AspNet.Identity;

    public interface IAuthDomain : IDisposable
    {
        #region Methods

        Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login);

        Task<bool> AddRefreshToken(RefreshToken token);

        Task<IdentityResult> CreateAsync(ApplicationUser user);

        Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo);

        Client FindClient(string clientId);

        Task<RefreshToken> FindRefreshToken(string refreshTokenId);

        Task<ApplicationUser> FindUser(string userName, string password);

        List<RefreshToken> GetAllRefreshTokens();

        Task<IdentityResult> RegisterUser(string userName, string password);

        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);

        Task<bool> RemoveRefreshToken(string refreshTokenId);

        #endregion Methods
    }
}