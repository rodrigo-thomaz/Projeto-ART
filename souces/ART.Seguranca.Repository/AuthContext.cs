namespace ART.Seguranca.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ART.Seguranca.Repository.Configurations;
    using ART.Seguranca.Repository.Entities;

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

        public DbSet<Application> Application
        {
            get; set;
        }

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

            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new UsersInApplicationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion Methods
    }
}