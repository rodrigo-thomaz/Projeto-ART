namespace RThomaz.Application.Financeiro.Models
{
    public class UsuarioSelectViewModel
    {
        private readonly long _usuarioId;
        private readonly string _email;
        private readonly string _nomeExibicao;
        private readonly string _avatarStorageObject;

        public UsuarioSelectViewModel(long usuarioId, string email, string nomeExibicao, string avatarStorageObject)
        {
            _usuarioId = usuarioId;
            _email = email;
            _nomeExibicao = nomeExibicao;            
            _avatarStorageObject = avatarStorageObject;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public string Email { get { return _email; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}