namespace ART.Seguranca.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Seguranca.Domain.Interfaces;
    using ART.Seguranca.Repository.Entities;
    using Microsoft.AspNet.Identity;
    using ART.Seguranca.Repository.Interfaces;
    using RabbitMQ.Client;
    using System.Configuration;
    using ART.Infra.CrossCutting.MQ;

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

            await InsertUserInDomotica(applicationUser);

            return identityResult;
        }

        private async Task InsertUserInDomotica(ApplicationUser applicationUser)
        {
            var model = _connection.CreateModel();
            var basicProperties = model.CreateBasicProperties();

            var queueName = ConfigurationManager.AppSettings["RabbitMQRegisterUserQueueName"];

            model.QueueDeclare(
                  queue: queueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            basicProperties.Persistent = true;

            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(applicationUser);

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