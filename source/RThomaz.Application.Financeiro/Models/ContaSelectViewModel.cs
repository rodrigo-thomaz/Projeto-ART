using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public abstract class ContaSelectViewModel
    {
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;

        public ContaSelectViewModel
            (
                  long contaId
                , TipoConta tipoConta
            )
        {
            _contaId = contaId;
            _tipoConta = tipoConta;
        }

        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
    }
}