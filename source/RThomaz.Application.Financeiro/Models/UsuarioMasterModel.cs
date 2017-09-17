namespace RThomaz.Application.Financeiro.Models
{
    public class UsuarioMasterModel
    {
        private readonly long _usuarioId;
        private readonly string _email;
        private readonly string _nomeExibicao;
        private readonly bool _ativo;
        private readonly string _avatarStorageObject;

        public UsuarioMasterModel(long usuarioId, string email, string nomeExibicao, bool ativo, string avatarStorageObject)
        {
            _usuarioId = usuarioId;
            _email = email;
            _nomeExibicao = nomeExibicao;
            _ativo = ativo;
            _avatarStorageObject = avatarStorageObject;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public string Email { get { return _email; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public bool Ativo { get { return _ativo; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}