namespace ART.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ARTDbContext>
    {
        #region Constructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "ARTDomoticaDbContext";
        }

        #endregion Constructors

        #region Methods

        protected override void Seed(ARTDbContext context)
        {
            Seeds.Execute(context);

            base.Seed(context);
        }

        #endregion Methods
    }
}