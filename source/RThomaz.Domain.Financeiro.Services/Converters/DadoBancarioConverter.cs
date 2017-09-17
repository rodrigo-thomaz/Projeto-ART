using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class DadoBancarioConverter
    {
        public static DadoBancarioDTO ConvertEntityToDTO(DadoBancario entity)
        {
            return new DadoBancarioDTO
            (
                numeroAgencia: entity.NumeroAgencia,
                numeroConta: entity.NumeroConta
            );
        }
    }
}
