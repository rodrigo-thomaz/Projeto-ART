using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class RateioConverter
    {
        public static List<RateioDetailViewDTO> ConvertEntityToDTO(ICollection<LancamentoRateio> entities)
        {
            var result = new List<RateioDetailViewDTO>();
            foreach (var entity in entities)
            {
                result.Add(ConvertEntityToDTO(entity));
            }
            return result;
        }

        public static RateioDetailViewDTO ConvertEntityToDTO(LancamentoRateio entity)
        {
            return new RateioDetailViewDTO
            (
                planoConta: PlanoContaConverter.ConvertEntityToDTO(entity.PlanoConta),
                centroCusto: CentroCustoConverter.ConvertEntityToDTO(entity.CentroCusto),
                observacao: entity.Observacao,
                porcentagem: entity.Porcentagem
            );
        }
    }
}
