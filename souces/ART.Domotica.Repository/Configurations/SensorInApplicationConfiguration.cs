using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    class SensorInApplicationConfiguration : EntityTypeConfiguration<SensorInApplication>
    {
        #region Constructors

        public SensorInApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.ApplicationId,
                x.SensorId,
            });

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.SensorInApplication)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);

            //SensorId
            Property(x => x.SensorId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_SensorId") { IsUnique = true },
                    }));

            //Sensor
            HasRequired(x => x.Sensor)
                .WithMany(x => x.SensorInApplication)
                .HasForeignKey(x => x.SensorId)
                .WillCascadeOnDelete(false);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(2)
                .IsRequired();

            //CreateByApplicationUserId
            Property(x => x.CreateByApplicationUserId)
                .HasColumnOrder(3);

            //CreateByApplicationUser
            HasRequired(x => x.CreateByApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.CreateByApplicationUserId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}