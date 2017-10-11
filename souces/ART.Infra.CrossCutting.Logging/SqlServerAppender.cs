using log4net.Appender;

namespace ART.Infra.CrossCutting.Logging
{
    using log4net.Layout;
    using System.Data;
    using System.Text;

    public class SqlServerAppender : AdoNetAppender
    {
        public SqlServerAppender()
        {
            Initialize();
        }

        private void Initialize()
        {
            CommandType = CommandType.Text;
            ConnectionType = GetConnectionType();
            ConnectionString = GetConnectionString();
            CommandText = GetCommandText();

            LoadParameters();                  
        }

        private string GetCommandText()
        {
            var sb = new StringBuilder();

            sb.Append("INSERT INTO Log(");
            sb.Append("[UtcDateTime]");
            sb.Append(",[Thread]");
            sb.Append(",[Level]");
            sb.Append(",[AppDomain]");
            sb.Append(",[Module]");
            sb.Append(",[Namespace]");
            sb.Append(",[Name]");
            sb.Append(",[Method]");
            sb.Append(",[Line]");
            sb.Append(",[Message]");
            sb.Append(",[Exception]");
            sb.Append(",[Identity]");
            sb.Append(",[StackTrace]");
            sb.Append(") VALUES(");
            sb.Append("@utcdatetime");
            sb.Append(", @thread");
            sb.Append(", @level");
            sb.Append(", @appdomain");
            sb.Append(", CASE WHEN @module = '' OR @module = '(null)' THEN NULL ELSE @module END");
            sb.Append(", @namespace");
            sb.Append(", @name");
            sb.Append(", @method");
            sb.Append(", CASE WHEN @line = 0 THEN NULL ELSE @line END");
            sb.Append(", CASE WHEN @message = '' OR @message = '(null)' THEN NULL ELSE @message END");
            sb.Append(", CASE WHEN @exception = '' OR @exception = '(null)' THEN NULL ELSE @exception END");
            sb.Append(", CASE WHEN @identity = '' OR @identity = '(null)' THEN NULL ELSE @identity END");
            sb.Append(", CASE WHEN @stacktrace = '' OR @stacktrace = '(null)' THEN NULL ELSE @stacktrace END");
            sb.Append(")");

            return sb.ToString();
        }

        private string GetConnectionType()
        {
            return "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        }

        private string GetConnectionString()
        {
            return @"data source=.\SQLEXPRESS;initial catalog=ART.Log;integrated security=false;persist security info=True;User ID=sa;Password=b3b3xu!@#";
        }

        private void LoadParameters()
        {
            AddDateTimeParameterToAppender("utcdatetime");
            AddStringParameterToAppender("thread", 255, "%thread");
            AddStringParameterToAppender("level", 10, "%level");
            AddStringParameterToAppender("appdomain", 500, "%appdomain");
            AddStringParameterToAppender("module", 500, "%property{callerModule}");
            AddStringParameterToAppender("namespace", 500, "%property{callerNamespace}");
            AddStringParameterToAppender("name", 500, "%property{callerName}");
            AddStringParameterToAppender("method", 500, "%property{callerMethod}");
            AddIntParameterToAppender("line", "%property{callerLine}");
            AddStringParameterToAppender("message", 4000, "%message");
            AddErrorParameterToAppender("exception", 4000);
            AddStringParameterToAppender("identity", 4000, "%identity");
            AddStringParameterToAppender("stacktrace", 500, "%property{callerStacktrace}");
        }

        private void AddStringParameterToAppender(string paramName, int size, string conversionPattern)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = DbType.String;
            param.Size = size;
            param.Layout = new Layout2RawLayoutAdapter(new PatternLayout(conversionPattern));
            AddParameter(param);
        }

        private void AddIntParameterToAppender(string paramName, string conversionPattern)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = DbType.Int32;
            param.Layout = new Layout2RawLayoutAdapter(new PatternLayout(conversionPattern));
            AddParameter(param);
        }

        private void AddDateTimeParameterToAppender(string paramName)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = DbType.DateTime;
            param.Layout = new RawUtcTimeStampLayout();
            AddParameter(param);
        }

        private void AddErrorParameterToAppender(string paramName, int size)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = DbType.String;
            param.Size = size;
            param.Layout = new Layout2RawLayoutAdapter(new ExceptionLayout());
            AddParameter(param);
        }
    }    
}
