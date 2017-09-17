using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoMasterListDTO
    {
        private readonly PagedListResponse<MovimentoMasterDTO> _pagedListResponse;
        private readonly decimal? _saldoAnterior;

        public MovimentoMasterListDTO
            (
                  PagedListResponse<MovimentoMasterDTO> pagedListResponse
                , decimal? saldoAnterior
            )
        {
            _pagedListResponse = pagedListResponse;
            _saldoAnterior = saldoAnterior;
        }

        public PagedListResponse<MovimentoMasterDTO> PagedListResponse { get { return _pagedListResponse; } }
        public decimal? SaldoAnterior { get { return _saldoAnterior; } }
    }
}