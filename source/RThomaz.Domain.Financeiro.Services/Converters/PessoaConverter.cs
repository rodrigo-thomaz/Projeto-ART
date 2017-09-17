using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class PessoaConverter
    {
        public static PessoaSelectViewDTO ConvertEntityToDTO(Pessoa entity)
        {
            var nome = string.Empty;

            switch (entity.TipoPessoa)
            {
                case TipoPessoa.PessoaFisica:
                    nome = ((PessoaFisica)entity).NomeCompleto;
                    break;
                case TipoPessoa.PessoaJuridica:
                    nome = ((PessoaJuridica)entity).NomeFantasia;
                    break;
                default:
                    throw new NotImplementedException();
            }

            var result = new PessoaSelectViewDTO
            (
                pessoaId: entity.PessoaId,
                tipoPessoa: entity.TipoPessoa,
                nome: nome
            );

            return result;
        }
    }
}
