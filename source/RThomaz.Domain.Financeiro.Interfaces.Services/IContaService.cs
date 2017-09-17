using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IContaService : IServiceBase
    {
        Task<ContaCartaoCreditoDetailViewDTO> EditContaCartaoCredito(ContaCartaoCreditoDetailEditDTO dto);
        Task<ContaCorrenteDetailViewDTO> EditContaCorrente(ContaCorrenteDetailEditDTO dto);
        Task<ContaEspecieDetailViewDTO> EditContaEspecie(ContaEspecieDetailEditDTO dto);
        Task<ContaPoupancaDetailViewDTO> EditContaPoupanca(ContaPoupancaDetailEditDTO dto);
        Task<ContaCartaoCreditoDetailViewDTO> GetContaCartaoCreditoDetail(long id);
        Task<ContaCorrenteDetailViewDTO> GetContaCorrenteDetail(long id);
        Task<ContaEspecieDetailViewDTO> GetContaEspecieDetail(long id);
        Task<ContaPoupancaDetailViewDTO> GetContaPoupancaDetail(long id);
        Task<PagedListResponse<ContaMasterDTO>> GetMasterList(PagedListRequest<ContaMasterDTO> pagedListRequest, TipoConta? tipoConta, bool? ativo);
        Task<List<ContaSelectViewDTO>> GetSelectViewList(TipoConta? tipoConta);
        Task<List<ContaSummaryViewDTO>> GetSummaryViewList();
        Task<List<CountDTO<ContaSelectViewDTO>>> GetWithProgramacaoSelectViewList();
        Task<ContaCartaoCreditoDetailViewDTO> InsertContaCartaoCredito(ContaCartaoCreditoDetailInsertDTO dto);
        Task<ContaCorrenteDetailViewDTO> InsertContaCorrente(ContaCorrenteDetailInsertDTO dto);
        Task<ContaEspecieDetailViewDTO> InsertContaEspecie(ContaEspecieDetailInsertDTO dto);
        Task<ContaPoupancaDetailViewDTO> InsertContaPoupanca(ContaPoupancaDetailInsertDTO dto);
        Task<bool> Remove(long contaId, TipoConta tipoConta);
    }
}