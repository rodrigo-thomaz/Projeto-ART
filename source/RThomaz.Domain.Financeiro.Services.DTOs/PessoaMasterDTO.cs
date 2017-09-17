using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaMasterDTO
    {
        private readonly long _pessoaId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public PessoaMasterDTO
            (
                  long pessoaId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _pessoaId = pessoaId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        public long PessoaId { get { return _pessoaId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}