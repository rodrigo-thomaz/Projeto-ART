namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Collections.Generic;

    public class HardwaresInApplicationConfiguration : EntityTypeConfiguration<HardwaresInApplication>
    {
        #region Constructors

        public HardwaresInApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_ApplicationId_HardwareBaseId", 0) { IsUnique = true },
                    }));

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.HardwaresInApplication)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);

            //HardwareBaseId
            Property(x => x.HardwareBaseId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_HardwareBaseId") { IsUnique = true },
                        new IndexAttribute("IX_Unique_ApplicationId_HardwareBaseId", 1) { IsUnique = true },
                    }));

            //HardwareBase
            HasRequired(x => x.HardwareBase)
                .WithMany(x => x.HardwaresInApplication)
                .HasForeignKey(x => x.HardwareBaseId)
                .WillCascadeOnDelete(false);            
        }

        #endregion Constructors
    }
}