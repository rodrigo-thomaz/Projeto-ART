namespace ART.Domotica.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ART.Domotica.Repository.Configurations;
    using ART.Domotica.Repository.Entities;

    public class ARTDbContext : DbContext
    {
        #region Constructors

        public ARTDbContext()
            : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=ARTDb;Integrated Security=false;User ID=sa;Password=b3b3xu!@#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False")
        {
            Initialize();
        }

        #endregion Constructors

        #region Properties

        public DbSet<Application> Application
        {
            get; set;
        }

        public DbSet<ApplicationUser> ApplicationUser
        {
            get; set;
        }

        public DbSet<DeviceInApplication> DeviceInApplication
        {
            get; set;
        }

        public DbSet<DSFamilyTempSensor> DSFamilyTempSensor
        {
            get; set;
        }

        public DbSet<DSFamilyTempSensorResolution> DSFamilyTempSensorResolution
        {
            get; set;
        }

        public DbSet<ESPDevice> ESPDevice
        {
            get; set;
        }

        public DbSet<HardwaresInProject> HardwaresInProject
        {
            get; set;
        }

        public DbSet<Project> Project
        {
            get; set;
        }

        public DbSet<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        public DbSet<TemperatureScale> TemperatureScale
        {
            get; set;
        }

        public DbSet<TempSensorRange> TempSensorRange
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configurations

            modelBuilder.Configurations.Add(new DeviceBaseConfiguration());
            modelBuilder.Configurations.Add(new DSFamilyTempSensorConfiguration());
            modelBuilder.Configurations.Add(new DSFamilyTempSensorResolutionConfiguration());
            modelBuilder.Configurations.Add(new ESPDeviceConfiguration());
            modelBuilder.Configurations.Add(new HardwareBaseConfiguration());
            modelBuilder.Configurations.Add(new RaspberryDeviceConfiguration());
            modelBuilder.Configurations.Add(new SensorBaseConfiguration());
            modelBuilder.Configurations.Add(new SensorsInDeviceConfiguration());
            modelBuilder.Configurations.Add(new DeviceInApplicationConfiguration());
            modelBuilder.Configurations.Add(new HardwaresInProjectConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new TemperatureScaleConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new TempSensorRangeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion Methods
    }
}