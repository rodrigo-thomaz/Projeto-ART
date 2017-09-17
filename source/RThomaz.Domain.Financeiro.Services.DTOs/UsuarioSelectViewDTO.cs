namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class UsuarioSelectViewDTO
    {
        private readonly long _usuarioId;
        private readonly string _nomeExibicao;
        private readonly string _email;
        private readonly string _avatarStorageObject;

        public UsuarioSelectViewDTO
            (
                  long usuarioId
                , string nomeExibicao
                , string email
                , string avatarStorageObject
            )
        {
            _usuarioId = usuarioId;
            _nomeExibicao = nomeExibicao;
            _email = email;
            _avatarStorageObject = avatarStorageObject;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public string Email { get { return _email; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}