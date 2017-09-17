namespace RThomaz.Application.Financeiro.Models
{
    public class ContaCartaoCreditoDetailInsertModel
    {
        public long BandeiraCartaoId { get; set; }

        public long? ContaCorrenteId { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }
    }
}