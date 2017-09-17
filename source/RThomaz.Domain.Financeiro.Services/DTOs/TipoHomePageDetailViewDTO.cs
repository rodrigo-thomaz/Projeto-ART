using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class TipoHomePageDetailDTO
    {
        private readonly long _tipoHomePageId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoHomePageDetailDTO
            (
                  long tipoHomePageId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoHomePageId = tipoHomePageId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        public long TipoHomePageId { get { return _tipoHomePageId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}