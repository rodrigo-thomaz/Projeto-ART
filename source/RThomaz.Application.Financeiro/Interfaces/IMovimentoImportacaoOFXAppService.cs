using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IMovimentoImportacaoOFXAppService : IAppServiceBase
    {
        Task<MovimentoImportacaoOFXModel> Preview(byte[] buffer);
        Task<MovimentoImportacaoDetailViewModel> Import(MovimentoImportacaoOFXModel model);
    }
}