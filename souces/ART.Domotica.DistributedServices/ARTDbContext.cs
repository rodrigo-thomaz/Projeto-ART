using ART.Domotica.DistributedServices.Configurations;
using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ART.Domotica.DistributedServices
{
    public class ARTDbContext : DbContext
    {
        public ARTDbContext()
             : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=ART.Domotica;Integrated Security=false;User ID=sa;Password=b3b3xu!@#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False")
        {
            Initialize();
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configurations

            modelBuilder.Configurations.Add(new DeviceBaseConfiguration());            
            modelBuilder.Configurations.Add(new DSFamilyTempSensorConfiguration());
            modelBuilder.Configurations.Add(new DSFamilyTempSensorResolutionConfiguration());
            modelBuilder.Configurations.Add(new ESPDeviceBaseConfiguration());
            modelBuilder.Configurations.Add(new HardwareBaseConfiguration());
            modelBuilder.Configurations.Add(new RaspberryDeviceBaseConfiguration());
            modelBuilder.Configurations.Add(new SensorBaseConfiguration());
            modelBuilder.Configurations.Add(new HardwareInSpaceConfiguration());
            modelBuilder.Configurations.Add(new SpaceConfiguration());
            modelBuilder.Configurations.Add(new ThermometerDeviceConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserInSpaceConfiguration());

            base.OnModelCreating(modelBuilder);
        }
                
        public DbSet<DSFamilyTempSensor> DSFamilyTempSensor { get; set; }
        public DbSet<DSFamilyTempSensorResolution> DSFamilyTempSensorResolution { get; set; }
        public DbSet<HardwareInSpace> HardwareInSpace { get; set; }
        public DbSet<Space> Space { get; set; }
        public DbSet<ThermometerDevice> ThermometerDevice { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInSpace> UserInSpace { get; set; }
    }
}