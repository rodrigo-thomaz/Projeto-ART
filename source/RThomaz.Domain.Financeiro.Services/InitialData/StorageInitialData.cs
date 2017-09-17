using RThomaz.Infra.CrossCutting.Storage;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RThomaz.Domain.Financeiro.Services.InitialData
{
    public class StorageInitialData
    {
        #region private fields
        
        private readonly StorageHelper _storageHelper;
        private readonly string _appStorageBucketName;

        #endregion

        #region constructors

        public StorageInitialData()
        {
            _storageHelper = new StorageHelper();
            _appStorageBucketName = ConfigurationManager.AppSettings["Google.AppStorageBucketName"];
        }

        #endregion

        #region public voids

        public async Task Seed()
        {
            var buckets = await _storageHelper.ListBucketsAsync();
            if (!buckets.Any(x => x.Name.Equals(_appStorageBucketName)))
            {
                await _storageHelper.InsertBucketAsync(StorageType.Standard, _appStorageBucketName);
            }
        }

        #endregion       
    }
}
