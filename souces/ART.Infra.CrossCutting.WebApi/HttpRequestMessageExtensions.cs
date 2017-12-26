namespace ART.Infra.CrossCutting.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    public static class HttpRequestMessageExtensions
    {
        #region Methods

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

        #endregion Methods
    }
}