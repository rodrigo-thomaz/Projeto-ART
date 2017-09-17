using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Interfaces.Services;

namespace RThomaz.Domain.Financeiro.Services
{
    public class CBOOcupacaoService : ICBOOcupacaoService
    {
        #region public voids

        public async Task<SelectListResponseDTO<CBOOcupacaoSelectViewDTO>> GetOcupacaoSelectViewList(SelectListRequestDTO selectListDTORequest)
        {
            SelectListResponseDTO<CBOOcupacaoSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<CBOOcupacao> query = context.CBOOcupacao
                    .OrderBy(x => x.Titulo);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x.Titulo.Contains(selectListDTORequest.Search));
                    query = query.Where(x =>
                            x.Codigo.Contains(selectListDTORequest.Search) ||
                            x.Titulo.Contains(selectListDTORequest.Search)
                        );
                }

                var totalRecords = await query.Select(x => x.CBOOcupacaoId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = new List<CBOOcupacaoSelectViewDTO>();
                
                foreach (var item in dataPaged)
                {
                    dtos.Add(new CBOOcupacaoSelectViewDTO
                    (
                        cboOcupacaoId: item.CBOOcupacaoId,
                        codigo: item.Codigo,
                        titulo: item.Titulo
                    ));
                }

                result = new SelectListResponseDTO<CBOOcupacaoSelectViewDTO>(
                     data: dtos,
                     totalRecords: totalRecords);
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return result;
        }

        public async Task<SelectListResponseDTO<CBOSinonimoSelectViewDTO>> GetSinonimoSelectViewList(SelectListRequestDTO selectListDTORequest, int cboOcupacaoId)
        {
            SelectListResponseDTO<CBOSinonimoSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<CBOSinonimo> query = context.CBOSinonimo
                    .Where(x => x.CBOOcupacaoId == cboOcupacaoId)
                    .OrderBy(x => x.Titulo);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x =>
                            x.Titulo.Contains(selectListDTORequest.Search)
                        );
                }

                var totalRecords = await query.Select(x => x.CBOOcupacaoId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = new List<CBOSinonimoSelectViewDTO>();

                foreach (var item in dataPaged)
                {
                    dtos.Add(new CBOSinonimoSelectViewDTO
                    (
                        cboSinonimoId: item.CBOSinonimoId,
                        titulo: item.Titulo
                    ));
                }

                result = new SelectListResponseDTO<CBOSinonimoSelectViewDTO>(
                     data: dtos,
                     totalRecords: totalRecords);
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return result;
        }

        #endregion
    }
}
