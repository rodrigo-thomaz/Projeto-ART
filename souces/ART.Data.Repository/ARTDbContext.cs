using ART.Data.Repository.Configurations;
using ART.Data.Repository.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ART.Data.Repository
{
    public class ARTDbContext : DbContext
    {
        #region constructors

        public ARTDbContext()
             : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=ARTDb;Integrated Security=false;User ID=sa;Password=b3b3xu!@#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False")
        {
            Initialize();
        }

        #endregion

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
            modelBuilder.Configurations.Add(new SensorsInDeviceConfiguration());
            modelBuilder.Configurations.Add(new HardwareInApplicationConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new TemperatureScaleConfiguration());
            modelBuilder.Configurations.Add(new ThermometerDeviceConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserInApplicationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
                
        public DbSet<DSFamilyTempSensor> DSFamilyTempSensor { get; set; }
        public DbSet<DSFamilyTempSensorResolution> DSFamilyTempSensorResolution { get; set; }
        public DbSet<SensorsInDevice> SensorsInDevice { get; set; }
        public DbSet<HardwareInApplication> HardwareInApplication { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<TemperatureScale> TemperatureScale { get; set; }
        public DbSet<ThermometerDevice> ThermometerDevice { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInApplication> UserInApplication { get; set; }
    }
}