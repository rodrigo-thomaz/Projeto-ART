using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ILocalidadeService : IServiceBase
    {
        Task<List<BairroSelectViewDTO>> GetBairroSelectViewList(long cidadeId);
        Task<List<CidadeSelectViewDTO>> GetCidadeSelectViewList(long estadoId);
        Task<List<EstadoSelectViewDTO>> GetEstadoSelectViewList(long paisId);
        Task<StorageDownloadDTO> GetPaisBandeira(string storageObject);
        Task<List<PaisSelectViewDTO>> GetPaisSelectViewList();
        Task<BairroSelectViewDTO> UpdateLocalidade(LocalidadeDetailUpdateDTO localidade);
    }
}