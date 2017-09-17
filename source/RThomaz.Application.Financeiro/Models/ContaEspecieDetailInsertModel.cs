namespace RThomaz.Application.Financeiro.Models
{
    public class ContaEspecieDetailInsertModel
    {
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public SaldoInicialModel SaldoInicial { get; set; }

        public string Descricao { get; set; }
    }
}