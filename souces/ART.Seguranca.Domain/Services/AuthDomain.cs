namespace ART.Seguranca.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Seguranca.Domain.Interfaces;
    using ART.Seguranca.Repository.Entities;
    using Microsoft.AspNet.Identity;
    using ART.Seguranca.Repository.Interfaces;

    public class AuthDomain : IAuthDomain
    {
        private readonly IAuthRepository _authRepository;

        public AuthDomain(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login)
        {
            return await _authRepository.AddLoginAsync(userId, login);
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            return await _authRepository.AddRefreshToken(token);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return await _authRepository.CreateAsync(user);
        }

        public void Dispose()
        {
            
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            return await _authRepository.FindAsync(loginInfo);
        }

        public Client FindClient(string clientId)
        {
            return _authRepository.FindClient(clientId);
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return await _authRepository.FindRefreshToken(refreshTokenId);
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            return await _authRepository.FindUser(userName, password);
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _authRepository.GetAllRefreshTokens();
        }

        public async Task<IdentityResult> RegisterUser(string userName, string password)
        {
            return await _authRepository.RegisterUser(userName, password);
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            return await _authRepository.RemoveRefreshToken(refreshToken);
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return await _authRepository.RemoveRefreshToken(refreshTokenId);
        }
    }
}