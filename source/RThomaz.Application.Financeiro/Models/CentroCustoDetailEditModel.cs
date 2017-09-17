namespace RThomaz.Application.Financeiro.Models
{
    public class CentroCustoDetailEditModel
    {
        public long CentroCustoId { get; set; }

        public long? ParentId { get; set; }

        public long? ResponsavelId { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }
    }
}