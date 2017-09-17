using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class BandeiraCartaoConverter
    {
        public static BandeiraCartaoSelectViewDTO ConvertEntityToDTO(BandeiraCartao entity)
        {
            return new BandeiraCartaoSelectViewDTO
            (
                bandeiraCartaoId: entity.BandeiraCartaoId,
                nome: entity.Nome,
                logoStorageObject: entity.LogoStorageObject
            );
        }
    }
}
