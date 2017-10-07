namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class HardwaresInProjectConfiguration : EntityTypeConfiguration<HardwaresInProject>
    {
        #region Constructors

        public HardwaresInProjectConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.HardwaresInApplicationId,
                x.ProjectId,
            });

            //HardwaresInApplicationId
            Property(x => x.HardwaresInApplicationId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //ProjectId
            Property(x => x.ProjectId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //HardwaresInApplication
            HasRequired(x => x.HardwaresInApplication)
                .WithMany(x => x.HardwaresInProject)
                .HasForeignKey(x => x.HardwaresInApplicationId)
                .WillCascadeOnDelete(false);

            //Project
            HasRequired(x => x.Project)
                .WithMany(x => x.HardwaresInProject)
                .HasForeignKey(x => x.ProjectId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}