namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class UsuarioMasterDTO
    {
        private readonly long _usuarioId;
        private readonly string _nomeExibicao;
        private readonly string _email;
        private readonly bool _ativo;
        private readonly string _avatarStorageObject;

        public UsuarioMasterDTO
            (
                  long usuarioId
                , string nomeExibicao
                , string email
                , bool ativo
                , string avatarStorageObject
            )
        {
            _usuarioId = usuarioId;
            _nomeExibicao = nomeExibicao;
            _email = email;
            _ativo = ativo;
            _avatarStorageObject = avatarStorageObject;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public string Email { get { return _email; } }
        public bool Ativo { get { return _ativo; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}