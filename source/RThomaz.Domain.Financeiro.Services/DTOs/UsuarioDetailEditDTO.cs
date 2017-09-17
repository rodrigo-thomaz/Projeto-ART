namespace RThomaz.Business.DTOs
{
    public class UsuarioEditDTO
    {
        private readonly long _usuarioId;
        private readonly bool _ativo;
        private readonly string _descricao;        

        public UsuarioEditDTO
            (
                  long usuarioId
                , bool ativo
                , string descricao
            )
        {
            _usuarioId = usuarioId;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }        
    }
}
