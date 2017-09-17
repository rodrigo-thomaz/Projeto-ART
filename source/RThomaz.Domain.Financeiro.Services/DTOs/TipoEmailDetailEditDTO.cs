using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class TipoEmailEditDTO
    {
        private readonly long _tipoEmailId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoEmailEditDTO
            (
                  long tipoEmailId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoEmailId = tipoEmailId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        public long TipoEmailId { get { return _tipoEmailId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}
