namespace ART.Infra.CrossCutting.Logging
{
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;

    public class ConsoleAppender : ColoredConsoleAppender
    {
        #region Constructors

        public ConsoleAppender()
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            AddMapping(new LevelColors
            {
                Level = Level.Debug,
                ForeColor = Colors.White,
            });

            AddMapping(new LevelColors
            {
                Level = Level.Info,
                ForeColor = Colors.Blue,
            });

            AddMapping(new LevelColors
            {
                Level = Level.Warn,
                ForeColor = Colors.Yellow,
            });

            AddMapping(new LevelColors
            {
                Level = Level.Error,
                ForeColor = Colors.Red,
            });

            AddMapping(new LevelColors
            {
                Level = Level.Fatal,
                ForeColor = Colors.White,
                BackColor = Colors.Red | Colors.HighIntensity,
            });

            Layout = new PatternLayout("[%-5level][%property{callerNamespace}.%property{callerName}.%property{callerMethod}]%message %exception [%property{NDC}] %newline");
        }

        #endregion Methods
    }
}