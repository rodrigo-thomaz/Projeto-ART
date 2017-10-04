using ART.Seguranca.DistributedServices.Configurations;
using ART.Seguranca.DistributedServices.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ART.Seguranca.DistributedServices
{
    public class AuthContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region constructors

        public AuthContext()
         : base("AuthContext")
        {
            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion

        #region protected voids

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configurations

            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new UsersInApplicationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region public voids

        public DbSet<Application> Application { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; } 

        #endregion
    }

}