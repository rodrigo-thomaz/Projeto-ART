using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoImportadoEditModel
    {
        public long MovimentoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public long MovimentoImportacaoId { get; set; }
        public string Observacao { get; set; }
    }
}