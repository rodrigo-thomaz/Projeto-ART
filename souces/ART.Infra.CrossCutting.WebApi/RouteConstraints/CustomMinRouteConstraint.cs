﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace ART.Infra.CrossCutting.WebApi.RouteConstraints
{
    public class CustomMinRouteConstraint : IHttpRouteConstraint
    {
        private readonly long _min;

        public CustomMinRouteConstraint(long min)
        {
            _min = min;
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
                    if (longValue >= _min)
                    {
                        return true;
                    }
                    var message = string.Format("The value of the '{0}' field must be greater than {1}", parameterName, _min);
                    throw new ArgumentOutOfRangeException(message, longValue, parameterName);
                }
            }

            return false;
        }
    }
}