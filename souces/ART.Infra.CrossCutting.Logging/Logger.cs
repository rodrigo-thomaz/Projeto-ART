using log4net;
using log4net.Core;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ART.Infra.CrossCutting.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog _log;

        public Logger(ILog log)
        {
            _log = log;
        }

        public void Debug([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
        {
            foreach (var method in new StackTrace().GetFrames())
            {                
                if (method.GetMethod().Name == callerMethod)
                {
                    var properties = LogicalThreadContext.Properties;
                    var type = method.GetMethod().ReflectedType;

                    properties["callerModule"] = type.Module.Name;
                    properties["callerNamespace"] = type.Namespace;
                    properties["callerName"] = type.Name;
                    properties["callerMethod"] = callerMethod;
                    properties["callerLine"] = callerLine;

                    callerMethod = $"{method.GetMethod().ReflectedType.Name}.{callerMethod}";
                    break;
                }
            }

            _log.Debug(null);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }
    }
}
