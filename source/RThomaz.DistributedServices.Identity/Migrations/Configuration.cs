namespace RThomaz.DistributedServices.Identity.Migrations
{
    using Infra.CrossCutting.Identity.Context;
    using Infra.CrossCutting.Identity.Entities;
    using Infra.CrossCutting.Identity.Enums;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Clients.AddOrUpdate(new Client
            {
                Id = "ngAuthApp",
                Secret = Helper.GetHash("abc@123"),
                Name = "RThomaz.Presentation.WebPages",
                ApplicationType = ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "http://localhost:33150"
            });

            context.Clients.AddOrUpdate(new Client
            {
                Id = "AppWebApi",
                Secret = Helper.GetHash("abc@123"),
                Name = "RThomaz.DistributedServices.Financeiro",
                ApplicationType = ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "http://localhost:45439"
            });

            context.Clients.AddOrUpdate(new Client
            {
                Id = "AppMCV",
                Secret = Helper.GetHash("abc@123"),
                Name = "RThomaz.Web",
                ApplicationType = ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "http://localhost:2125"
            });

            context.Clients.AddOrUpdate(new Client
            {
                Id = "consoleApp",
                Secret = Helper.GetHash("123@abc"),
                Name = "Console Application",
                ApplicationType = ApplicationTypes.NativeConfidential,
                Active = true,
                RefreshTokenLifeTime = 14400,
                AllowedOrigin = "*"
            });

            context.SaveChanges();
        }
    }
}
