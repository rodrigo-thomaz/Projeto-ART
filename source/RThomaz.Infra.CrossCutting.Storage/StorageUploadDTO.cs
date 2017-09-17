using System;

namespace RThomaz.Infra.CrossCutting.Storage
{
    public class StorageUploadDTO
    {
        private readonly byte[] _buffer;
        private readonly string _contentType;

        public StorageUploadDTO
        (
              string contentType
            , byte[] buffer
        )
        {
            if(string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException("contentType");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            _contentType = contentType;
            _buffer = buffer;
        }

        public byte[] Buffer { get { return _buffer; } }
        public string ContentType { get { return _contentType; } }
    }
}