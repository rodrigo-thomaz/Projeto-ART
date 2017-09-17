namespace RThomaz.Infra.CrossCutting.Storage
{
    public class StorageDownloadDTO
    {
        private readonly byte[] _buffer;
        private readonly string _contentType;

        public StorageDownloadDTO
            (
                  string fileName
                , byte[] data
            )
        {
            _contentType = fileName;
            _buffer = data;
        }

        public byte[] Buffer { get { return _buffer; } }
        public string ContentType { get { return _contentType; } }
    }
}