namespace ART.Infra.CrossCutting.Logging
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    using log4net;

    public class Logger : ILogger
    {
        #region Fields

        private readonly ILog _log;

        #endregion Fields

        #region Constructors

        public Logger(ILog log)
        {
            _log = log;
        }

        #endregion Constructors

        #region Methods

        public void Debug([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Debug(null);
        }

        public void Error([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Error(null);
        }

        public void Fatal([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Fatal(null);
        }

        public void Info([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Info(null);
        }

        public void Warn([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Warn(null);
        }

        public void Debug(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Debug(message);
        }

        public void Error(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Error(message);
        }

        public void Fatal(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Fatal(message);
        }

        public void Info(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Info(message);
        }

        public void Warn(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Warn(message);
        }
        
        public void Debug(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Debug(message, ex);
        }

        public void Error(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Error(message, ex);
        }

        public void Fatal(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Fatal(message, ex);
        }

        public void Info(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Info(message, ex);
        }

        public void Warn(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine);
            _log.Warn(message, ex);
        }

        private void Prepare([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            var frames = new StackTrace().GetFrames();

            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i].GetMethod().Name == callerMethod)
                {
                    var stackTrace = new StackTrace(i);

                    var callerStackTrace = "";

                    foreach (var tempFrame in stackTrace.GetFrames())
                    {
                        var tempMethod = tempFrame.GetMethod();
                        callerStackTrace += tempMethod.Name + Environment.NewLine;
                    }

                    var properties = LogicalThreadContext.Properties;
                    var type = frames[i].GetMethod().ReflectedType;

                    properties["callerModule"] = type.Module.Name;
                    properties["callerNamespace"] = type.Namespace;
                    properties["callerName"] = type.Name;
                    properties["callerMethod"] = callerMethod;
                    properties["callerLine"] = callerLine;
                    properties["callerStackTrace"] = callerStackTrace;

                    break;
                }
            }
        }

        #endregion Methods
    }
}