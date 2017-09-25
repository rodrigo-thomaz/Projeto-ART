namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DSFamilyTempSensor", "DeviceAddress", c => c.String(nullable: false, maxLength: 15,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Unique_DeviceAddress",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            CreateIndex("dbo.DSFamilyTempSensor", "DeviceAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            AlterColumn("dbo.DSFamilyTempSensor", "DeviceAddress", c => c.String(nullable: false, maxLength: 15,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Unique_DeviceAddress",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
        }
    }
}
