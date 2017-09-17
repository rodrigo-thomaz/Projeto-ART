using RThomaz.Business.Helpers.Storage;
namespace RThomaz.Business.DTOs
{
    public class BandeiraCartaoEditDTO
    {
        private readonly long _bandeiraCartaoId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly StorageUploadDTO _arquivo;
        private readonly string _logoStorageObject;

        public BandeiraCartaoEditDTO
            (
                  long bandeiraCartaoId
                , string nome
                , bool ativo
                , StorageUploadDTO arquivo
                , string logoStorageObject
            )
        {
            _bandeiraCartaoId = bandeiraCartaoId;
            _nome = nome;
            _ativo = ativo;
            _arquivo = arquivo;
            _logoStorageObject = logoStorageObject;
        }

        public long BandeiraCartaoId { get { return _bandeiraCartaoId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public StorageUploadDTO Arquivo { get { return _arquivo; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}
