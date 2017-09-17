using RThomaz.Domain.Financeiro.Services.DTOs;
using System.Threading.Tasks;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IMovimentoImportacaoOFXService : IServiceBase
    {
        Task<MovimentoImportacaoOFXDTO> Preview(byte[] buffer);
        Task<MovimentoImportacaoDetailViewDTO> Import(MovimentoImportacaoOFXDTO dto);
    }
}