using RThomaz.Infra.CrossCutting.Storage;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BandeiraCartaoDetailInsertDTO
    {
        private readonly string _nome;
        private readonly bool _ativo;

        private readonly StorageUploadDTO _storageUpload;

        public BandeiraCartaoDetailInsertDTO
            (
                  string nome
                , bool ativo
                , StorageUploadDTO storageUpload
            )
        {
            _nome = nome;
            _ativo = ativo;
            _storageUpload = storageUpload;
        }

        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public StorageUploadDTO StorageUpload { get { return _storageUpload; } }
    }
}
