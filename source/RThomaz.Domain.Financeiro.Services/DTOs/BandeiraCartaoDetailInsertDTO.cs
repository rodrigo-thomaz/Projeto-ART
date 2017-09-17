using RThomaz.Business.Helpers.Storage;
namespace RThomaz.Business.DTOs
{
    public class BandeiraCartaoInsertDTO
    {
        private readonly string _nome;
        private readonly bool _ativo;

        private readonly StorageUploadDTO _arquivo;

        public BandeiraCartaoInsertDTO
            (
                  string nome
                , bool ativo
                , StorageUploadDTO arquivo
            )
        {
            _nome = nome;
            _ativo = ativo;
            _arquivo = arquivo;
        }

        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public StorageUploadDTO Arquivo { get { return _arquivo; } }
    }
}
