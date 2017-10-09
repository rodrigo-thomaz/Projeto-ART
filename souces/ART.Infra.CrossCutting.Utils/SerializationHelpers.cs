namespace ART.Infra.CrossCutting.Utils
{
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class SerializationHelpers
    {
        #region Methods

        public static T DeserializeJsonBufferToType<T>(byte[] value)
            where T : class
        {
            var json = Encoding.UTF8.GetString(value);
            var result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return result;
        }

        public static byte[] SerializeToJsonBufferAsync(object value)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(value, settings);
            var result = Encoding.UTF8.GetBytes(json);
            return result;
        }

        #endregion Methods
    }
}