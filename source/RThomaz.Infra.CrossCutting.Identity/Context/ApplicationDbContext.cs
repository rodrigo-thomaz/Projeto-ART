using System;
using Microsoft.AspNet.Identity.EntityFramework;
using RThomaz.Infra.CrossCutting.Identity.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RThomaz.Infra.CrossCutting.Identity.Configurations;

namespace RThomaz.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IDisposable
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Initialize();
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configurations

            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ApplicationRoleConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new RefreshTokenConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Application> Application { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}