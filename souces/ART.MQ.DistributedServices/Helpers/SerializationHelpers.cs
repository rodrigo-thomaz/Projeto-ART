using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ART.MQ.DistributedServices.Helpers
{
    public class SerializationHelpers
    {
        public static byte[] SerialiseIntoBinary<T>(T model)
        {
            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, model);
            memoryStream.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            byte[] result = memoryStream.GetBuffer();
            memoryStream.Close();
            memoryStream.Dispose();
            return result;
        }
    }
}