using ART.Seguranca.Repository.Entities;
using ART.Seguranca.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ART.Seguranca.Repository.Repositories
{

    public class AuthRepository : IAuthRepository
    {
        private readonly AuthContext _authContext;

        private UserManager<ApplicationUser, Guid> _userManager;

        public AuthRepository(AuthContext authContext)
        {
            _authContext = authContext;
            _userManager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(_authContext));
        }

        public async Task<IdentityResult> RegisterUser(ApplicationUser applicationUser, string password)
        {           
            var result = await _userManager.CreateAsync(applicationUser, password);            
            return result;
        }        

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _authContext.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _authContext.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }
          
            _authContext.RefreshTokens.Add(token);

            return await _authContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _authContext.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _authContext.RefreshTokens.Remove(refreshToken);
               return await _authContext.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _authContext.RefreshTokens.Remove(refreshToken);
             return await _authContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _authContext.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _authContext.RefreshTokens.ToList();
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
            _authContext.Dispose();
            _userManager.Dispose();

        }
    }
}