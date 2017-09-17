using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaSummaryViewDTO
    {
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly ContaSelectViewDTO _conta;
        private readonly decimal _saldoAtual;

        public ContaSummaryViewDTO
        (
              long contaId
            , TipoConta tipoConta
            , ContaSelectViewDTO conta
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
        public ContaSelectViewDTO Conta { get { return _conta; } }
        public decimal SaldoAtual { get { return _saldoAtual; } }
    }
}
