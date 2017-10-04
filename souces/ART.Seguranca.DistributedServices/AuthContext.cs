using ART.Seguranca.DistributedServices.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace ART.Seguranca.DistributedServices
{
    public class AuthContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public AuthContext()
            : base("AuthContext")
        {
     
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

}