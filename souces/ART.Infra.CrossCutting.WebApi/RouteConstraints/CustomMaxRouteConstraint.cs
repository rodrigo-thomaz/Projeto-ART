namespace ART.Infra.CrossCutting.WebApi.RouteConstraints
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public class CustomMaxRouteConstraint : IHttpRouteConstraint
    {
        #region Fields

        private readonly long _max;

        #endregion Fields

        #region Constructors

        public CustomMaxRouteConstraint(long max)
        {
            _max = max;
        }

        #endregion Constructors

        #region Methods

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                var stringValue = value as string;
                long longValue = 0;

                if (stringValue != null && long.TryParse(stringValue, out longValue))
                {
                    if (longValue <= _max)
                    {
                        return true;
                    }
                    var message = string.Format("The value of the '{0}' field must be less than '{1}'", parameterName, _max);
                    throw new ArgumentOutOfRangeException(message, longValue, parameterName);
                }
            }

            return false;
        }

        #endregion Methods
    }
}