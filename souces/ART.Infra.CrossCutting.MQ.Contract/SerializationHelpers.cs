using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace ART.Infra.CrossCutting.MQ.Contract
{
    public static class SerializationHelpers
    {
        public static byte[] SerializeToJsonBufferAsync(object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var result = Encoding.UTF8.GetBytes(json);
            return result;
        }

        public static T DeserializeJsonBufferToType<T>(byte[] value)
            where T : class
        {
            var json = Encoding.UTF8.GetString(value);
            var result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return result;
        }
    }
}
