using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RThomaz.Infra.CrossCutting.Storage
{
    public class StorageHelper
    {
        #region private fields
        
        private readonly string _projectId;
        private readonly string _location;
        private readonly StorageService _storageService; 

        #endregion

        #region private const

        private const string StandardStorageClass = "STANDARD";
        private const string NearlineStorageClass = "NEARLINE";
        private const string DurableReducedAvailabilityStorageClass = "DURABLE_REDUCED_AVAILABILITY"; 

        #endregion

        #region constructors

        public StorageHelper()
        {
            _projectId = ConfigurationManager.AppSettings["Google.ProjectId"];
            _location = ConfigurationManager.AppSettings["Google.Location"];

            if (string.IsNullOrEmpty(_projectId))
            {
                throw new ArgumentException("Google.ProjectId required");
            }

            if (string.IsNullOrEmpty(_location))
            {
                throw new ArgumentException("Google.Location required");
            }
            
            _storageService = CreateStorageClient();
        }

        #endregion

        #region public voids

        public IList<Bucket> ListBuckets()
        {
            var buckets = _storageService.Buckets.List(_projectId).Execute();
            return buckets.Items;
        }

        public async Task<IList<Bucket>> ListBucketsAsync()
        {
            var buckets = await _storageService.Buckets.List(_projectId).ExecuteAsync();
            return buckets.Items;
        }

        public Bucket GetBucket(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException("bucketName required");
            }

            return _storageService.Buckets.Get(bucketName).Execute();
        }

        public async Task<Bucket> GetBucketAsync(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException("bucketName required");
            }

            return await _storageService.Buckets.Get(bucketName).ExecuteAsync();
        }

        public Bucket InsertBucket(StorageType type, string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException("bucketName required");
            }

            var bucket = new Bucket();

            bucket.Location = _location;
            bucket.Name = bucketName;
            bucket.StorageClass = ConvertToStorageClass(type);

            return _storageService.Buckets.Insert(bucket, _projectId).Execute();
        }

        public async Task<Bucket> InsertBucketAsync(StorageType type, string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException("bucketName required");
            }

            var bucket = new Bucket();

            bucket.Location = _location;
            bucket.Name = bucketName;
            bucket.StorageClass = ConvertToStorageClass(type);

            return await _storageService.Buckets.Insert(bucket, _projectId).ExecuteAsync();
        }

        public void UploadBuffer(string bucketName, string storageObject, string contentType, byte[] data)
        {
            using (var uploadStream = new MemoryStream(data))
            {
                _storageService.Objects.Insert(
                    bucket: bucketName,
                    stream: uploadStream,
                    contentType: contentType,
                    body: new Google.Apis.Storage.v1.Data.Object() { Name = storageObject }
                ).Upload();
            }
        }

        public async Task UploadBufferAsync(string bucketName, string storageObject, string contentType, byte[] data)
        {
            using (var uploadStream = new MemoryStream(data))
            {
                await _storageService.Objects.Insert(
                    bucket: bucketName,
                    stream: uploadStream,
                    contentType: contentType,
                    body: new Google.Apis.Storage.v1.Data.Object() { Name = storageObject }
                ).UploadAsync();
            }
        }

        public StorageDownloadDTO DownloadBuffer(string bucketName, string storageObject)
        {
            var contentType = _storageService.Objects.Get(bucketName, storageObject).Execute().ContentType;
            byte[] buffer = null;
            using (var stream = new MemoryStream())
            {
                _storageService.Objects.Get(bucketName, storageObject).Download(stream);
                buffer = stream.GetBuffer();
            }
            return new StorageDownloadDTO(contentType, buffer);
        }

        public async Task<StorageDownloadDTO> DownloadBufferAsync(string bucketName, string storageObject)
        {
            var contentType = _storageService.Objects.Get(bucketName, storageObject).Execute().ContentType;
            byte[] buffer = null;
            using (var stream = new MemoryStream())
            {
                await _storageService.Objects.Get(bucketName, storageObject).DownloadAsync(stream);
                buffer = stream.GetBuffer();
            }
            return new StorageDownloadDTO(contentType, buffer);
        }

        public void DeleteObject(string bucketName, string storageObject)
        {
            _storageService.Objects.Delete(bucketName, storageObject).Execute();
        }

        public async Task DeleteObjectAsync(string bucketName, string storageObject)
        {
            await _storageService.Objects.Delete(bucketName, storageObject).ExecuteAsync();
        }

        #endregion

        #region private voids

        private StorageService CreateStorageClient()
        {
            var credentials = GoogleCredential.GetApplicationDefaultAsync().Result;

            if (credentials.IsCreateScopedRequired)
            {
                credentials = credentials.CreateScoped(new[] { StorageService.Scope.DevstorageFullControl });
            }

            var serviceInitializer = new BaseClientService.Initializer()
            {
                ApplicationName = "RThomaz Storage",
                HttpClientInitializer = credentials
            };

            return new StorageService(serviceInitializer);
        }

        private string ConvertToStorageClass(StorageType type)
        {
            switch (type)
            {
                case StorageType.Standard:
                    return StandardStorageClass;
                case StorageType.Nearline:
                    return NearlineStorageClass;
                case StorageType.DurableReducedAvailability:
                    return DurableReducedAvailabilityStorageClass;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region samples        

        public void ListObjectsSample(string bucketName)
        {
            var objects = _storageService.Objects.List(bucketName).Execute();

            if (objects.Items != null)
            {
                foreach (var obj in objects.Items)
                {
                    Console.WriteLine("Object: {obj.Name}");
                }
            }
        }

        public void UploadStreamSample(string bucketName, string storageObject, string contentType, string content)
        {
            var data = Encoding.UTF8.GetBytes(content);
            using (var uploadStream = new MemoryStream(data))
            {
                _storageService.Objects.Insert(
                    bucket: bucketName,
                    stream: uploadStream,
                    contentType: contentType,
                    body: new Google.Apis.Storage.v1.Data.Object() { Name = storageObject }
                ).Upload();
            }
        }

        public void DownloadStreamSample(string bucketName)
        {
            using (var stream = new MemoryStream())
            {
                _storageService.Objects.Get(bucketName, "texte.txt").Download(stream);

                var content = Encoding.UTF8.GetString(stream.GetBuffer());

                Console.WriteLine("Downloaded my-file.txt with content: {content}");
            }
        }

        public void DownloadToFileSample(string bucketName)
        {
            var objectToDownload = _storageService.Objects.Get(bucketName, "my-file.txt").Execute();

            var downloader = new MediaDownloader(_storageService);

            downloader.ProgressChanged += progress =>
            {
                Console.WriteLine("{progress.Status} {progress.BytesDownloaded} bytes");
            };

            using (var fileStream = new FileStream("downloaded-file.txt", FileMode.Create))
            {
                var progress = downloader.Download(objectToDownload.MediaLink, fileStream);

                if (progress.Status == DownloadStatus.Completed)
                {
                    Console.WriteLine("Downloaded my-file.txt to downloaded-file.txt");
                }
            }
        } 

        #endregion
    }
}
