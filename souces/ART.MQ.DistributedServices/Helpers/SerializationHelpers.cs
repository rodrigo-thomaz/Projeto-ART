using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ART.MQ.DistributedServices.Helpers
{
    public class SerializationHelpers
    {
        public static async Task<byte[]> SerialiseIntoBinaryAsync<T>(T model)
        {
            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, model);
            await memoryStream.FlushAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);
            byte[] result = memoryStream.GetBuffer();
            memoryStream.Close();
            memoryStream.Dispose();
            return result;
        }
    }
}