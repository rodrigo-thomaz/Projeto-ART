using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace RThomaz.DistributedServices.Core.RouteConstraints
{
    public class CustomRangeRouteConstraint : IHttpRouteConstraint
    {
        private readonly long _min;
        private readonly long _max;

        public CustomRangeRouteConstraint(long min, long max)
        {
            _min = min;
            _max = max;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                var stringValue = value as string;
                long longValue = 0;

                if (stringValue != null && long.TryParse(stringValue, out longValue))
                {
                    if (longValue >= _min && longValue <= _max)
                    {
                        return true;
                    }
                    var message = string.Format("The value of the '{0}' field must be between {1} and {2}", parameterName, _min, _max);
                    throw new ArgumentOutOfRangeException(message, longValue, parameterName);
                }
            }

            return false;
        }
    }
}
