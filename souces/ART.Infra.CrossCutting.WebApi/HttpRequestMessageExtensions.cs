namespace ART.Infra.CrossCutting.WebApi
{
    using System.Net.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public static class HttpRequestMessageExtensions
    {
        public static T GetFirstHeaderValueOrDefault<T>(
            this HttpRequestMessage request,
            string headerKey)
        {
            var toReturn = default(T);

            IEnumerable<string> headerValues;

            if (request.Headers.TryGetValues(headerKey, out headerValues))
            {
                var valueString = headerValues.FirstOrDefault();
                if (valueString != null)
                {
                    return (T)Convert.ChangeType(valueString, typeof(T));
                }
            }

            return toReturn;
        }
    }
}
