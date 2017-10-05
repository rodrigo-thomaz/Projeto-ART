namespace ART.Data.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ART.Data.Repository.Configurations;
    using ART.Data.Repository.Entities;

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

        public DbSet<SensorsInDevice> SensorsInDevice { get; set; }

        public DbSet<Application> Application
        {
            get; set;
        }

        public DbSet<ApplicationUser> ApplicationUser
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

        public DbSet<TemperatureScale> TemperatureScale
        {
            get; set;
        }

        public DbSet<ThermometerDevice> ThermometerDevice
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
            modelBuilder.Configurations.Add(new ESPDeviceBaseConfiguration());
            modelBuilder.Configurations.Add(new HardwareBaseConfiguration());
            modelBuilder.Configurations.Add(new RaspberryDeviceBaseConfiguration());
            modelBuilder.Configurations.Add(new SensorBaseConfiguration());
            modelBuilder.Configurations.Add(new SensorsInDeviceConfiguration());
            modelBuilder.Configurations.Add(new HardwaresInApplicationConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new TemperatureScaleConfiguration());
            modelBuilder.Configurations.Add(new ThermometerDeviceConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new UsersInApplicationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion Methods
    }
}