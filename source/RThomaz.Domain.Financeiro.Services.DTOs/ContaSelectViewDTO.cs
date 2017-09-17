using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public abstract class ContaSelectViewDTO
    {
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;

        public ContaSelectViewDTO
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