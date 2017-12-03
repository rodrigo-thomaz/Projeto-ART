namespace ART.Domotica.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ART.Domotica.Repository.Configurations;
    using ART.Domotica.Repository.Configurations.Locale;
    using ART.Domotica.Repository.Configurations.SI;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Entities.Locale;
    using ART.Domotica.Repository.Entities.SI;

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

        public DbSet<ActuatorType> ActuatorType
        {
            get; set;
        }

        public DbSet<Application> Application
        {
            get; set;
        }

        public DbSet<ApplicationMQ> ApplicationMQ
        {
            get; set;
        }

        public DbSet<ApplicationUser> ApplicationUser
        {
            get; set;
        }

        public DbSet<Continent> Continent
        {
            get; set;
        }

        public DbSet<Country> Country
        {
            get; set;
        }

        public DbSet<DeviceInApplication> DeviceInApplication
        {
            get; set;
        }

        public DbSet<DeviceMQ> DeviceMQ
        {
            get; set;
        }

        public DbSet<DeviceNTP> DeviceNTP
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

        public DbSet<NumericalScale> NumericalScale
        {
            get; set;
        }

        public DbSet<NumericalScalePrefix> NumericalScalePrefix
        {
            get; set;
        }

        public DbSet<NumericalScaleType> NumericalScaleType
        {
            get; set;
        }

        public DbSet<NumericalScaleTypeCountry> NumericalScaleTypeCountry
        {
            get; set;
        }

        public DbSet<Project> Project
        {
            get; set;
        }

        public DbSet<Sensor> Sensor
        {
            get; set;
        }

        public DbSet<SensorChartLimiter> SensorChartLimiter
        {
            get; set;
        }

        public DbSet<SensorDatasheet> SensorDatasheet
        {
            get; set;
        }

        public DbSet<SensorDatasheetUnitMeasurementScale> SensorDatasheetUnitMeasurementScale
        {
            get; set;
        }

        public DbSet<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        public DbSet<SensorTrigger> SensorTrigger
        {
            get; set;
        }

        public DbSet<SensorType> SensorType
        {
            get; set;
        }

        public DbSet<SensorUnitMeasurementDefault> SensorUnitMeasurementDefault
        {
            get; set;
        }

        public DbSet<TimeZone> TimeZone
        {
            get; set;
        }

        public DbSet<UnitMeasurement> UnitMeasurement
        {
            get; set;
        }

        public DbSet<UnitMeasurementScale> UnitMeasurementScale
        {
            get; set;
        }

        public DbSet<UnitMeasurementType> UnitMeasurementType
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

            //SI
            modelBuilder.Configurations.Add(new NumericalScaleTypeConfiguration());
            modelBuilder.Configurations.Add(new NumericalScaleTypeCountryConfiguration());
            modelBuilder.Configurations.Add(new NumericalScalePrefixConfiguration());
            modelBuilder.Configurations.Add(new NumericalScaleConfiguration());
            modelBuilder.Configurations.Add(new UnitMeasurementConfiguration());
            modelBuilder.Configurations.Add(new UnitMeasurementScaleConfiguration());
            modelBuilder.Configurations.Add(new UnitMeasurementTypeConfiguration());

            modelBuilder.Configurations.Add(new ActuatorTypeConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new ApplicationMQConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ContinentConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new DeviceBaseConfiguration());
            modelBuilder.Configurations.Add(new DeviceInApplicationConfiguration());
            modelBuilder.Configurations.Add(new DeviceMQConfiguration());
            modelBuilder.Configurations.Add(new DeviceNTPConfiguration());
            modelBuilder.Configurations.Add(new DSFamilyTempSensorConfiguration());
            modelBuilder.Configurations.Add(new DSFamilyTempSensorResolutionConfiguration());
            modelBuilder.Configurations.Add(new ESPDeviceConfiguration());
            modelBuilder.Configurations.Add(new HardwareBaseConfiguration());
            modelBuilder.Configurations.Add(new HardwaresInProjectConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new RaspberryDeviceConfiguration());
            modelBuilder.Configurations.Add(new SensorChartLimiterConfiguration());
            modelBuilder.Configurations.Add(new SensorConfiguration());
            modelBuilder.Configurations.Add(new SensorDatasheetConfiguration());
            modelBuilder.Configurations.Add(new SensorsInDeviceConfiguration());
            modelBuilder.Configurations.Add(new SensorTriggerConfiguration());
            modelBuilder.Configurations.Add(new SensorTypeConfiguration());
            modelBuilder.Configurations.Add(new SensorUnitMeasurementDefaultConfiguration());
            modelBuilder.Configurations.Add(new SensorDatasheetUnitMeasurementScaleConfiguration());
            modelBuilder.Configurations.Add(new TimeZoneConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion Methods
    }
}