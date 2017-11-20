namespace ART.Infra.CrossCutting.Utils
{
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class SerializationHelpers
    {
        #region Methods

        public static T DeserializeJsonBufferToType<T>(byte[] value, bool stringEnumConverter = false)
            where T : class
        {
            var json = Encoding.UTF8.GetString(value);
            var result = JsonConvert.DeserializeObject<T>(json, CreateJsonSerializerSettings(stringEnumConverter));
            return result;
        }

        public static byte[] SerializeToJsonBufferAsync(object value, bool stringEnumConverter = false)
        {
            var json = JsonConvert.SerializeObject(value, CreateJsonSerializerSettings(stringEnumConverter));
            var result = Encoding.UTF8.GetBytes(json);
            return result;
        }

        private static JsonSerializerSettings CreateJsonSerializerSettings(bool stringEnumConverter)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            if (stringEnumConverter)
            {
                settings.Converters.Add(new StringEnumConverter());
            }
            return settings;
        }

        #endregion Methods
    }
}