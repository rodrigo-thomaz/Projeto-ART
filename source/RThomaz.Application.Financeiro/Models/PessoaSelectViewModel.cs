using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class PessoaSelectViewModel
    {
        private readonly long _pessoaId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public PessoaSelectViewModel
            (
                  long pessoaId
                , TipoPessoa tipoPessoa
                , string nome
            )
        {
            _pessoaId = pessoaId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
        }

        public long PessoaId { get { return _pessoaId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
    }
}