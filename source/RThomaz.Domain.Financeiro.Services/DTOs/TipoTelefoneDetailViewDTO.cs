using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class TipoTelefoneDetailDTO
    {
        private readonly long _tipoTelefoneId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoTelefoneDetailDTO
            (
                  long tipoTelefoneId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoTelefoneId = tipoTelefoneId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        public long TipoTelefoneId { get { return _tipoTelefoneId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}