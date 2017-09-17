using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoPagoRecebidoMasterListDTO
    {
        private readonly PagedListResponse<LancamentoPagoRecebidoMasterDTO> _pagedListResponse;
        private readonly decimal _saldoAnterior;

        public LancamentoPagoRecebidoMasterListDTO
            (
                  PagedListResponse<LancamentoPagoRecebidoMasterDTO> pagedListResponse
                , decimal saldoAnterior
            )
        {
            _pagedListResponse = pagedListResponse;
            _saldoAnterior = saldoAnterior;
        }

        public PagedListResponse<LancamentoPagoRecebidoMasterDTO> PagedListResponse { get { return _pagedListResponse; } }
        public decimal SaldoAnterior { get { return _saldoAnterior; } }
    }
}