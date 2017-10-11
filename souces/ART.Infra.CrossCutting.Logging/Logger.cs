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

        public void DebugEnter([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine, false);
            _log.Debug("Enter");
        }

        public void DebugLeave([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            Prepare(callerMethod, callerLine, false);
            _log.Debug("Leave");
        }

        private void Prepare(string callerMethod, int callerLine, bool hasStackTrace)
        {
            var frames = new StackTrace().GetFrames();

            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i].GetMethod().Name == callerMethod)
                {
                    var callerStackTrace = "";

                    if (hasStackTrace)
                    {
                        var stackTrace = new StackTrace(i);
                        foreach (var tempFrame in stackTrace.GetFrames())
                        {
                            var tempMethod = tempFrame.GetMethod();
                            callerStackTrace += tempMethod.Name + Environment.NewLine;
                        }
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