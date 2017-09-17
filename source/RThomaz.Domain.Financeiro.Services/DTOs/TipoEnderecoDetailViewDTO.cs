using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class TipoEnderecoDetailDTO
    {
        private readonly long _tipoEnderecoId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoEnderecoDetailDTO
            (
                  long tipoEnderecoId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoEnderecoId = tipoEnderecoId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        public long TipoEnderecoId { get { return _tipoEnderecoId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}