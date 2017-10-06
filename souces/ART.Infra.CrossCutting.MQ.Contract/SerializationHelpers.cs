using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ART.Infra.CrossCutting.MQ.Contract
{
    public static class SerializationHelpers
    {
        public static async Task<byte[]> SerializeToJsonBufferAsync(object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, json);
            await memoryStream.FlushAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);
            byte[] result = memoryStream.GetBuffer();
            memoryStream.Close();
            memoryStream.Dispose();
            return result;
        }

        public static T DeserializeJsonBufferToType<T>(byte[] value)
        {
            T result;
            using (MemoryStream ms = new MemoryStream(value))
            {
                IFormatter br = new BinaryFormatter();
                string json = br.Deserialize(ms).ToString();
                result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            return result;
        }
    }
}
