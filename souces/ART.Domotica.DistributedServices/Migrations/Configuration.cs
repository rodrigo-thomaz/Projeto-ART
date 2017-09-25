namespace ART.Domotica.DistributedServices.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ARTDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "ARTDomoticaDbContext";
        }

        protected override void Seed(ARTDbContext context)
        {
            Seeds.Execute(context);

            base.Seed(context);
        }
    }
}
