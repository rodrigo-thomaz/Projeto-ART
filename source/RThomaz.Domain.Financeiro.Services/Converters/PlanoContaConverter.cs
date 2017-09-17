using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class PlanoContaConverter
    {
        public static List<PlanoContaSelectViewDTO> ConvertEntityToDTO(IEnumerable<PlanoConta> entities)
        {
            if (!entities.Any())
            {
                throw new ArgumentOutOfRangeException("entities");
            }

            var result = new List<PlanoContaSelectViewDTO>();

            foreach (var entity in entities)
            {
                result.Add(ConvertEntityToDTO(entity));
            }

            return result;
        }

        public static PlanoContaSelectViewDTO ConvertEntityToDTO(PlanoConta entity)
        {            
            return new PlanoContaSelectViewDTO
            (
                planoContaId: entity.PlanoContaId,
                nome: entity.Nome,
                tipoTransacao: entity.TipoTransacao,
                children: entity.Children == null ? null : ConvertEntityToDTO(entity.Children)
            );
        }
    }
}
