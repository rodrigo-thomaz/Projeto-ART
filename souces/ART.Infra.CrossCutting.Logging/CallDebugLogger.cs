namespace ART.Infra.CrossCutting.Logging
{
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
            //var arguments = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());

            var targetType = invocation.TargetType;
            var targetMethod = invocation.MethodInvocationTarget;

            var properties = LogicalThreadContext.Properties;

            var module = targetType.Module == null ? "" : targetType.Module.Name;

            properties["callerModule"] = module;
            properties["callerNamespace"] = targetType.Namespace;
            properties["callerName"] = targetType.Name;
            properties["callerMethod"] = targetMethod.Name;
            properties["callerLine"] = 0;
            properties["callerStackTrace"] = "";

            _log.Debug("Enter");

            invocation.Proceed();

            if (invocation.ReturnValue == null)
            {
                _log.Debug("Leave: no results.");
            }
            else if (invocation.ReturnValue.GetType().BaseType != null && invocation.ReturnValue.GetType().BaseType == typeof(Task))
            {
                _log.Debug("Leave");
            }
            else
            {
                _log.DebugFormat("Leave: result was {0}.", invocation.ReturnValue);                
            }            
        }

        #endregion Methods
    }
}