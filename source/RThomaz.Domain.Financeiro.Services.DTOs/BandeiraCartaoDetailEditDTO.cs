using RThomaz.Infra.CrossCutting.Storage;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BandeiraCartaoDetailEditDTO
    {
        private readonly long _bandeiraCartaoId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly StorageUploadDTO _storageUpload;
        private readonly string _logoStorageObject;

        public BandeiraCartaoDetailEditDTO
            (
                  long bandeiraCartaoId
                , string nome
                , bool ativo
                , StorageUploadDTO storageUpload
                , string logoStorageObject
            )
        {
            _bandeiraCartaoId = bandeiraCartaoId;
            _nome = nome;
            _ativo = ativo;
            _storageUpload = storageUpload;
            _logoStorageObject = logoStorageObject;
        }

        public long BandeiraCartaoId { get { return _bandeiraCartaoId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public StorageUploadDTO StorageUpload { get { return _storageUpload; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}
