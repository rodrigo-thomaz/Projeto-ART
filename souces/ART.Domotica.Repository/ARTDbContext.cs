namespace ART.Domotica.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Reflection;

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

        public DbSet<DeviceBinary> DeviceBinary
        {
            get; set;
        }

        public DbSet<DeviceDatasheet> DeviceDatasheet
        {
            get; set;
        }

        public DbSet<DeviceDatasheetBinary> DeviceDatasheetBinary
        {
            get; set;
        }

        public DbSet<DeviceDatasheetBinaryBuffer> DeviceDatasheetBinaryBuffer
        {
            get; set;
        }

        public DbSet<DeviceDebug> DeviceDebug
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

        public DbSet<DeviceSensor> DeviceSensor
        {
            get; set;
        }

        public DbSet<DeviceSerial> DeviceSerial
        {
            get; set;
        }

        public DbSet<DeviceType> DeviceType
        {
            get; set;
        }

        public DbSet<DeviceWiFi> DeviceWiFi
        {
            get; set;
        }

        public DbSet<ESPDevice> ESPDevice
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

        public DbSet<Sensor> Sensor
        {
            get; set;
        }

        public DbSet<SensorDatasheet> SensorDatasheet
        {
            get; set;
        }

        public DbSet<SensorDatasheetUnitMeasurementDefault> SensorDatasheetUnitMeasurementDefault
        {
            get; set;
        }

        public DbSet<SensorDatasheetUnitMeasurementScale> SensorDatasheetUnitMeasurementScale
        {
            get; set;
        }

        public DbSet<SensorInApplication> SensorInApplication
        {
            get; set;
        }

        public DbSet<SensorInDevice> SensorInDevice
        {
            get; set;
        }

        public DbSet<SensorTempDSFamily> SensorTempDSFamily
        {
            get; set;
        }

        public DbSet<SensorTempDSFamilyResolution> SensorTempDSFamilyResolution
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

        public DbSet<SensorUnitMeasurementScale> SensorUnitMeasurementScale
        {
            get; set;
        }

        public DbSet<Entities.Globalization.TimeZone> TimeZone
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

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion Methods
    }
}