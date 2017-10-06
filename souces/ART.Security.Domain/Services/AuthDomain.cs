namespace ART.Security.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Security.Domain.Interfaces;
    using ART.Security.Repository.Entities;
    using Microsoft.AspNet.Identity;
    using ART.Security.Repository.Interfaces;
    using RabbitMQ.Client;
    using global::AutoMapper;
    using ART.Security.Common.Contracts;
    using ART.Security.Common.QueueNames;

    public class AuthDomain : IAuthDomain
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConnection _connection;

        public AuthDomain(IAuthRepository authRepository, IConnection connection)
        {
            _authRepository = authRepository;
            _connection = connection;
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
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName
            };

            var identityResult = await _authRepository.RegisterUser(applicationUser, password);

            await SendRegisterUserToBroker(applicationUser);

            return identityResult;
        }

        private async Task SendRegisterUserToBroker(ApplicationUser applicationUser)
        {
            var model = _connection.CreateModel();
            var basicProperties = model.CreateBasicProperties();

            var queueName = ApplicationUserQueueName.RegisterUserQueueName;

            model.QueueDeclare(
                  queue: queueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            basicProperties.Persistent = true;

            var contract = Mapper.Map<ApplicationUser, ApplicationUserContract>(applicationUser);

            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);

            await Task.Run(() => model.BasicPublish("", queueName, null, payload));            
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