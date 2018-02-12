namespace ART.Domotica.Repository.Configurations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceSerialConfiguration : EntityTypeConfiguration<DeviceSerial>
    {
        #region Constructors

        public DeviceSerialConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.DeviceTypeId,
                x.DeviceDatasheetId,
                x.DeviceId,
                x.Id,
            });

            //DeviceTypeId
            Property(x => x.DeviceTypeId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("FK_DeviceSerial_DeviceBase_DeviceTypeId_DeviceId_DeviceDatasheetId", 0),
                        new IndexAttribute("IX_Unique_DeviceSerialIndex", 0) { IsUnique = true },
                    }));

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("FK_DeviceSerial_DeviceBase_DeviceTypeId_DeviceId_DeviceDatasheetId", 1),
                        new IndexAttribute("IX_Unique_DeviceSerialIndex", 1) { IsUnique = true },
                    }));

            //DeviceId
            Property(x => x.DeviceId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("FK_DeviceSerial_DeviceBase_DeviceTypeId_DeviceId_DeviceDatasheetId", 2),
                        new IndexAttribute("IX_Unique_DeviceSerialIndex", 2) { IsUnique = true },
                    }));

            //Id
            Property(x => x.Id)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Unique_DeviceSerialIndex", 3) { IsUnique = true }));

            //Index
            Property(x => x.Index)
                .HasColumnOrder(4)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Unique_DeviceSerialIndex", 4) { IsUnique = true }));

            //DeviceBase
            HasRequired(x => x.DeviceBase)
                .WithMany(x => x.DeviceSerial)
                .HasForeignKey(x => new
                {
                    x.DeviceTypeId,
                    x.DeviceDatasheetId,
                    x.DeviceId,
                })
                .WillCascadeOnDelete(false);

            //Enabled
            Property(x => x.Enabled)
                .HasColumnOrder(5)
                .IsRequired();

            //SerialMode
            Property(x => x.SerialMode)
                .HasColumnOrder(6)
                .IsRequired();

            //AllowPinSwapRX
            Property(x => x.AllowPinSwapRX)
                .HasColumnOrder(7)
                .IsOptional();

            //AllowPinSwapTX
            Property(x => x.AllowPinSwapTX)
                .HasColumnOrder(8)
                .IsOptional();

            //PinRX
            Property(x => x.PinRX)
                .HasColumnOrder(9)
                .IsOptional();

            //PinTX
            Property(x => x.PinTX)
                .HasColumnOrder(10)
                .IsOptional();

            //BaudRate
            Property(x => x.BaudRate)
                .HasColumnOrder(11)
                .IsRequired();
        }

        #endregion Constructors
    }
}