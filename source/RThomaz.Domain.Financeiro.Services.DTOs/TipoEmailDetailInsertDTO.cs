using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoEmailDetailInsertDTO
    {
        private readonly string _nome;
        private readonly TipoPessoa _tipoPessoa;
        private readonly bool _ativo;

        public TipoEmailDetailInsertDTO
        (
              string nome
            , TipoPessoa tipoPessoa
            , bool ativo
        )
        {
            _nome = nome;
            _tipoPessoa = tipoPessoa;
            _ativo = ativo;
        }

        public string Nome { get { return _nome; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public bool Ativo { get { return _ativo; } }
    }
}
