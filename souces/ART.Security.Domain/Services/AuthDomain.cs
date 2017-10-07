namespace ART.Security.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Security.Domain.Interfaces;
    using ART.Security.Repository.Entities;
    using Microsoft.AspNet.Identity;
    using ART.Security.Repository.Interfaces;
    using global::AutoMapper;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Security.Contract;
    using ART.Security.Domain.IProducers;

    public class AuthDomain : IAuthDomain
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthProducer _authProducer;

        public AuthDomain(IAuthRepository authRepository, IAuthProducer authProducer)
        {
            _authRepository = authRepository;
            _authProducer = authProducer;
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

        public async Task<IdentityResult> RegisterUser(NoAuthenticatedMessageContract message, string userName, string password)
        {            
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName
            };

            var identityResult = await _authRepository.RegisterUser(applicationUser, password);

            await SendRegisterUserToBroker(message, applicationUser);

            return identityResult;
        }

        private async Task SendRegisterUserToBroker(NoAuthenticatedMessageContract message, ApplicationUser applicationUser)
        {
            var contract = Mapper.Map<ApplicationUser, RegisterUserContract>(applicationUser);
            await _authProducer.RegisterUser(new NoAuthenticatedMessageContract<RegisterUserContract>
            {
                Contract = contract,
                SouceMQSession = message.SouceMQSession,
            });
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