namespace ART.Security.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ART.Security.Repository.Configurations;
    using ART.Security.Repository.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class AuthContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region Constructors

        public AuthContext()
            : base("AuthContext")
        {
            Initialize();
        }

        #endregion Constructors

        #region Properties

        public DbSet<Client> Clients
        {
            get; set;
        }

        public DbSet<RefreshToken> RefreshTokens
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configurations

            modelBuilder.Configurations.Add(new ApplicationRoleConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserClaimConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion Methods
    }
}