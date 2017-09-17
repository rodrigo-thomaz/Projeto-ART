using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoPagarReceberMasterListDTO
    {
        private readonly PagedListResponse<LancamentoPagarReceberMasterDTO> _pagedListResponse;
        private readonly decimal _saldoAnterior;

        public LancamentoPagarReceberMasterListDTO
            (
                  PagedListResponse<LancamentoPagarReceberMasterDTO> pagedListResponse
                , decimal saldoAnterior
            )
        {
            _pagedListResponse = pagedListResponse;
            _saldoAnterior = saldoAnterior;
        }

        public PagedListResponse<LancamentoPagarReceberMasterDTO> PagedListResponse { get { return _pagedListResponse; } }
        public decimal SaldoAnterior { get { return _saldoAnterior; } }
    }
}