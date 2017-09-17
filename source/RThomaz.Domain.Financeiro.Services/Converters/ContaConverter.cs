using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class ContaConverter
    {
        public static ContaSelectViewDTO ConvertEntityToDTO(Conta entity)
        {
            ContaSelectViewDTO result = null;

            switch (entity.TipoConta)
            {
                case TipoConta.ContaEspecie:
                    result = ConvertEntityToDTO(entity as ContaEspecie);
                    break;
                case TipoConta.ContaCorrente:
                    result = ConvertEntityToDTO(entity as ContaCorrente);
                    break;
                case TipoConta.ContaPoupanca:
                    result = ConvertEntityToDTO(entity as ContaPoupanca);
                    break;
                case TipoConta.ContaCartaoCredito:
                    result = ConvertEntityToDTO(entity as ContaCartaoCredito);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }

        public static ContaEspecieSelectViewDTO ConvertEntityToDTO(ContaEspecie entity)
        {
            return new ContaEspecieSelectViewDTO
            (
                contaId: entity.ContaId,
                tipoConta: entity.TipoConta,
                nome: entity.Nome
            );
        }

        public static ContaCorrenteSelectViewDTO ConvertEntityToDTO(ContaCorrente entity)
        {
            return new ContaCorrenteSelectViewDTO
            (
                contaId: entity.ContaId,
                tipoConta: entity.TipoConta,
                banco: BancoConverter.ConvertEntityToDTO(entity.Banco),
                dadoBancario: DadoBancarioConverter.ConvertEntityToDTO(entity.DadoBancario)
            );
        }

        public static ContaPoupancaSelectViewDTO ConvertEntityToDTO(ContaPoupanca entity)
        {
            return new ContaPoupancaSelectViewDTO
            (
                contaId: entity.ContaId,
                tipoConta: entity.TipoConta,
                banco: BancoConverter.ConvertEntityToDTO(entity.Banco),
                dadoBancario: DadoBancarioConverter.ConvertEntityToDTO(entity.DadoBancario)
            );
        }

        public static ContaCartaoCreditoSelectViewDTO ConvertEntityToDTO(ContaCartaoCredito entity)
        {
            return new ContaCartaoCreditoSelectViewDTO
            (
                contaId: entity.ContaId,
                tipoConta: entity.TipoConta,
                nome: entity.Nome,
                bandeiraCartao: BandeiraCartaoConverter.ConvertEntityToDTO(entity.BandeiraCartao),
                contaCorrente: entity.ContaCorrente == null ? null : ConvertEntityToDTO(entity.ContaCorrente)
            );
        }

        public static CountDTO<ContaSelectViewDTO> ConvertEntityToDTO(Conta entity, long count)
        {
            CountDTO<ContaSelectViewDTO> result = null;

            switch (entity.TipoConta)
            {
                case TipoConta.ContaEspecie:
                    result = new CountDTO<ContaSelectViewDTO>(ConvertEntityToDTO(entity as ContaEspecie), count);
                    break;
                case TipoConta.ContaCorrente:
                    result = new CountDTO<ContaSelectViewDTO>(ConvertEntityToDTO(entity as ContaCorrente), count);
                    break;
                case TipoConta.ContaPoupanca:
                    result = new CountDTO<ContaSelectViewDTO>(ConvertEntityToDTO(entity as ContaPoupanca), count);
                    break;
                case TipoConta.ContaCartaoCredito:
                    result = new CountDTO<ContaSelectViewDTO>(ConvertEntityToDTO(entity as ContaCartaoCredito), count);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }
    }
}
