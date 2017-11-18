namespace ART.Infra.CrossCutting.Utils
{
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json.Converters;

    public static class SerializationHelpers
    {
        #region Methods

        public static T DeserializeJsonBufferToType<T>(byte[] value)
            where T : class
        {
            var json = Encoding.UTF8.GetString(value);
            var result = JsonConvert.DeserializeObject<T>(json, CreateJsonSerializerSettings());
            return result;
        }

        public static byte[] SerializeToJsonBufferAsync(object value)
        {
            var json = JsonConvert.SerializeObject(value, CreateJsonSerializerSettings());
            var result = Encoding.UTF8.GetBytes(json);
            return result;
        }

        private static JsonSerializerSettings CreateJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }

        #endregion Methods
    }
}