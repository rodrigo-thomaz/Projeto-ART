namespace RThomaz.Application.Financeiro.Models
{
    public class ContaCorrenteDetailInsertModel
    {
        public long BancoId { get; set; }

        public DadoBancarioModel DadoBancario { get; set; }

        public bool Ativo { get; set; }

        public SaldoInicialModel SaldoInicial { get; set; }

        public string Descricao { get; set; }
    }
}