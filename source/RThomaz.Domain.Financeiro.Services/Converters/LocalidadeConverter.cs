using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class LocalidadeConverter
    {
        public static PaisSelectViewDTO ConvertEntityToDTO(Pais entity)
        {
            var result = new PaisSelectViewDTO
                (
                    paisId: entity.PaisId,
                    nome: entity.Nome,
                    iso2: entity.ISO2,
                    iso3: entity.ISO3,
                    numCode: entity.NumCode,
                    ddi: entity.DDI,
                    cctld: entity.ccTLD,
                    bandeiraStorageObject: entity.BandeiraStorageObject
                );

            return result;
        }

        public static EstadoSelectViewDTO ConvertEntityToDTO(Estado entity)
        {
            var result = new EstadoSelectViewDTO
                (
                    estadoId: entity.EstadoId,
                    nome: entity.Nome,
                    sigla: entity.Sigla,
                    pais: ConvertEntityToDTO(entity.Pais)
                );

            return result;
        }

        public static CidadeSelectViewDTO ConvertEntityToDTO(Cidade entity)
        {
            var result = new CidadeSelectViewDTO
                (
                    cidadeId: entity.CidadeId,
                    nome: entity.Nome,
                    nomeAbreviado: entity.NomeAbreviado,
                    estado: ConvertEntityToDTO(entity.Estado)
                );

            return result;
        }

        public static BairroSelectViewDTO ConvertEntityToDTO(Bairro entity)
        {
            var result = new BairroSelectViewDTO
                (
                    bairroId: entity.BairroId,
                    nome: entity.Nome,
                    nomeAbreviado: entity.NomeAbreviado,
                    cidade: ConvertEntityToDTO(entity.Cidade)
                );

            return result;
        }
    }
}
