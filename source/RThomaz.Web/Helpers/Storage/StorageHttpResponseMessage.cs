using RThomaz.Infra.CrossCutting.Storage;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RThomaz.Web.Helpers.Storage
{
    public class StorageHttpResponseMessage : HttpResponseMessage
    {
        public StorageHttpResponseMessage(StorageDownloadDTO dto) : base(HttpStatusCode.OK)
        {
            var stream = new MemoryStream(dto.Buffer);

            Content = new StreamContent(stream);
            Content.Headers.ContentType = new MediaTypeHeaderValue(dto.ContentType);
            Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        }
    }
}