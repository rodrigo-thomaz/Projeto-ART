using RThomaz.Application.Financeiro.Models;
using RThomaz.Infra.CrossCutting.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ILocalidadeAppService : IAppServiceBase
    {
        Task<List<BairroSelectViewModel>> GetBairroSelectViewList(long cidadeId);
        Task<List<CidadeSelectViewModel>> GetCidadeSelectViewList(long estadoId);
        Task<List<EstadoSelectViewModel>> GetEstadoSelectViewList(long paisId);
        Task<StorageDownloadDTO> GetPaisBandeira(string storageObject);
        Task<List<PaisSelectViewModel>> GetPaisSelectViewList();
        Task<BairroSelectViewModel> UpdateLocalidade(LocalidadeDetailUpdateModel localidade);
    }
}