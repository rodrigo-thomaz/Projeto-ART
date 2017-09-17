namespace RThomaz.Infra.Data.Persistence.Migrations
{
    using Contexts;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RThomazDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "RThomaz.Infra.Data.Persistence.Contexts.RThomazDbContext";
        }

        protected override void Seed(RThomazDbContext context)
        {
                       
        }
    }
}
