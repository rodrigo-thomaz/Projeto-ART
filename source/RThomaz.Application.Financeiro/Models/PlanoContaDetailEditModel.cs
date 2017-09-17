using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class PlanoContaDetailEditModel
    {
        public long PlanoContaId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public long? ParentId { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }
    }
}