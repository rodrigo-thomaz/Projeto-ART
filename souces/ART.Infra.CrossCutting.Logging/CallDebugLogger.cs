namespace ART.Infra.CrossCutting.Logging
{
    using System.Linq;
    using System.Threading.Tasks;

    using Castle.DynamicProxy;

    using log4net;

    public class CallDebugLogger : IInterceptor
    {
        #region Fields

        ILog _log;

        #endregion Fields

        #region Constructors

        public CallDebugLogger(ILog log)
        {
            _log = log;
        }

        #endregion Constructors

        #region Methods

        public void Intercept(IInvocation invocation)
        {
            var arguments = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());

            var targetType = invocation.TargetType;
            var targetMethod = invocation.MethodInvocationTarget;

            var properties = LogicalThreadContext.Properties;

            if(targetType.Module.Name.Contains("Consumer"))
            {

            }

            properties["callerModule"] = targetType.Module.Name;
            properties["callerNamespace"] = targetType.Namespace;
            properties["callerName"] = targetType.Name;
            properties["callerMethod"] = targetMethod.Name;
            properties["callerLine"] = 0;
            properties["callerStackTrace"] = "";

            _log.DebugFormat("Calling");

            invocation.Proceed();

            if (invocation.ReturnValue == null)
            {
                _log.Debug("Done: no results.");
            }
            else if (invocation.ReturnValue.GetType().BaseType != null && invocation.ReturnValue.GetType().BaseType == typeof(Task))
            {
                _log.Debug("Done task");
            }
            else
            {
                _log.DebugFormat("Done: result was {0}.", invocation.ReturnValue);
            }
        }

        #endregion Methods
    }
}