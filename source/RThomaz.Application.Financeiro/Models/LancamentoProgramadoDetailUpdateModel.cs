using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoProgramadoDetailUpdateModel : ProgramacaoDetailUpdateBaseModel
    {
        public long ProgramacaoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public List<RateioDetailUpdateModel> Rateios { get; set; }

        public long? PessoaId { get; set; }

        public TipoPessoa? TipoPessoa { get; set; }

        public PessoaSelectViewModel PessoaSelectView { get; set; }

        public long ContaId { get; set; }

        public TipoConta TipoConta { get; set; }
    }
}