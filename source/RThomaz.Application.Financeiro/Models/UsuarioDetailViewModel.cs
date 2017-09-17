namespace RThomaz.Application.Financeiro.Models
{
    public class UsuarioDetailViewModel
    {
        private readonly long _usuarioId;
        private readonly string _nomeExibicao;
        private readonly string _email;
        private readonly bool _ativo;
        private readonly string _avatarStorageObject;
        private readonly string _descricao;

        public UsuarioDetailViewModel
            (
                  long usuarioId
                , string nomeExibicao
                , string email
                , bool ativo
                , string avatarStorageObject
                , string descricao
            )
        {
            _usuarioId = usuarioId;
            _nomeExibicao = nomeExibicao;
            _email = email;
            _ativo = ativo;
            _avatarStorageObject = avatarStorageObject;
            _descricao = descricao;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public string Email { get { return _email; } }
        public bool Ativo { get { return _ativo; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
        public string Descricao { get { return _descricao; } }
    }
}