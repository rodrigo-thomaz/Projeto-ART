using ART.MQ.Worker.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ART.MQ.Worker.Configurations
{
    public class SpaceConfiguration : EntityTypeConfiguration<Space>
    {
        public SpaceConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Name
            Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Description
            Property(x => x.Description)
                .HasMaxLength(5000)
                .IsOptional();
        }
    }
}