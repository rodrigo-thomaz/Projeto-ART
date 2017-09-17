using RThomaz.DistributedServices.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Infra.CrossCutting.Identity.Managers;
using RThomaz.Infra.CrossCutting.Identity.Entities;
using RThomaz.Infra.CrossCutting.Identity.Context;
using System.Text;
using System.Data.Entity;
using RThomaz.Infra.CrossCutting.Storage;

namespace RThomaz.DistributedServices.Identity
{

    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _ctx;

        private ApplicationUserManager _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var bucketName = string.Format("rthomaz-client-{0}", Guid.NewGuid().ToString().ToLower());

            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.UserName,
                DisplayName = userModel.UserName,
                Application = new Application
                {
                    StorageBucketName = bucketName,
                    Active = true,
                }
            };
            
            var result = await _userManager.CreateAsync(user, userModel.Password);

            //Storage

            var storageHelper = new StorageHelper();

            var insertBucket = storageHelper.InsertBucket(StorageType.Standard, bucketName);

            //Integração

            var sql = new StringBuilder();

            sql.AppendFormat("INSERT INTO Aplicacao (AplicacaoId) VALUES ('{0}');"
                    , user.Application.ApplicationId);

            sql.AppendFormat("INSERT INTO Usuario (AplicacaoId, ApplicationUserId, Email, Senha, Ativo) VALUES ('{0}', '{1}', '{2}', '{3}', {4});"
                    , user.Application.ApplicationId
                    , user.Id
                    , user.Email
                    , "Obsolete"
                    , Convert.ToInt32(user.Application.Active));

            sql.AppendFormat("INSERT INTO Perfil (UsuarioId, AplicacaoId, NomeExibicao, AvatarStorageObject) VALUES (@@IDENTITY, '{0}', '{1}', '{2}');"
                    , user.Application.ApplicationId
                    , user.DisplayName
                    , string.Empty);

            await _ctx.Database.ExecuteSqlCommandAsync(sql.ToString());
            
            return result;
        }

        public long GetUsuarioId(Guid applicationUserId)
        {
            var usuarioId = _ctx.Database.SqlQuery<long>(string.Format("SELECT UsuarioId FROM Usuario WHERE ApplicationUserId = '{0}'", applicationUserId)).FirstOrDefault();
            return usuarioId;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<Application> FindApplication(Guid applicationId)
        {
            Application result = await _ctx.Application.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
            return result;
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _ctx.RefreshTokens.Remove(refreshToken);
               return await _ctx.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
             return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _ctx.RefreshTokens.ToList();
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}