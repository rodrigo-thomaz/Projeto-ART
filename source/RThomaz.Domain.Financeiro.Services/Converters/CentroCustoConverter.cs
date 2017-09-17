using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class CentroCustoConverter
    {
        public static List<CentroCustoSelectViewDTO> ConvertEntityToDTO(IEnumerable<CentroCusto> entities)
        {
            if(!entities.Any())
            {
                throw new ArgumentOutOfRangeException("entities");
            }

            var result = new List<CentroCustoSelectViewDTO>();

            foreach (var entity in entities)
            {
                result.Add(ConvertEntityToDTO(entity));
            }

            return result;
        }

        public static CentroCustoSelectViewDTO ConvertEntityToDTO(CentroCusto entity)
        {            
            return new CentroCustoSelectViewDTO
            (
                centroCustoId: entity.CentroCustoId,
                nome: entity.Nome,
                children: entity.Children == null ? null : ConvertEntityToDTO(entity.Children)
            );
        }
    }
}
