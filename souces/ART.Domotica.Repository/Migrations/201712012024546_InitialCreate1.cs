namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SI.NumericalScale", "ScientificNotationBase", c => c.Decimal(nullable: false, precision: 24, scale: 12));
            AlterColumn("SI.NumericalScale", "ScientificNotationExponent", c => c.Decimal(nullable: false, precision: 24, scale: 12));
        }
        
        public override void Down()
        {
            AlterColumn("SI.NumericalScale", "ScientificNotationExponent", c => c.Short(nullable: false));
            AlterColumn("SI.NumericalScale", "ScientificNotationBase", c => c.Short(nullable: false));
        }
    }
}
