using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class BancoConverter
    {
        public static BancoSelectViewDTO ConvertEntityToDTO(Banco entity)
        {
            return new BancoSelectViewDTO
            (
                bancoId: entity.BancoId,
                nome: entity.Nome,
                numero: entity.Numero,
                logoStorageObject: entity.LogoStorageObject
            );
        }
    }
}
