namespace RThomaz.Business.DTOs
{
    public class PerfilPersonalInfoDTO
    {
        private readonly long _usuarioId;
        private readonly string _nomeExibicao;

        public PerfilPersonalInfoDTO
            (
                  long usuarioId
                , string nomeExibicao
            )
        {
            _usuarioId = usuarioId;
            _nomeExibicao = nomeExibicao;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public string NomeExibicao{ get { return _nomeExibicao; } }
    }
}
