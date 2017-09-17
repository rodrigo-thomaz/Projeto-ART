using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class ContaSummaryViewModel
    {
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly ContaSelectViewModel _conta;
        private readonly decimal _saldoAtual;

        public ContaSummaryViewModel
        (
              long contaId
            , TipoConta tipoConta
            , ContaSelectViewModel conta
            , decimal saldoAtual
        )
        {
            _contaId = contaId;
            _tipoConta = tipoConta;
            _conta = conta;
            _saldoAtual = saldoAtual;
        }

        public long ContaId { get { return _contaId; } }

        public TipoConta TipoConta { get { return _tipoConta; } }

        public ContaSelectViewModel Conta { get { return _conta; } }

        public decimal SaldoAtual { get { return _saldoAtual; } }
    }
}