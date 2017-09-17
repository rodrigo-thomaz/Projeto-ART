using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.Helpers.Ofx;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Threading.Tasks;
using System.Data.Entity;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class MovimentoImportacaoOFXService : ServiceBase, IMovimentoImportacaoOFXService
    {
        #region constructors

        public MovimentoImportacaoOFXService()
        {

        }

        #endregion

        #region public voids             

        public async Task<MovimentoImportacaoDetailViewDTO> Import(MovimentoImportacaoOFXDTO dto)
        {
            MovimentoImportacaoDetailViewDTO result = null;

            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                var movimentoImportacao = context.MovimentoImportacao.Add(new MovimentoImportacao
                {
                    AplicacaoId = AplicacaoId,
                    ImportadoEm = DateTime.Now
                });

                var movimentacoesDB = await context.Movimento
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.DataMovimento >= dto.DataInicio)
                    .Where(x => x.DataMovimento <= dto.DataFim)
                    .Where(x => x.ContaId.Equals(dto.ContaId))
                    .Where(x => x.TipoConta == dto.TipoConta)
                    .ToListAsync();

                foreach (var item in dto.Movimentacoes)
                {
                    var movimento = movimentacoesDB
                        .Where(x => x.DataMovimento.Equals(item.DataMovimento))
                        .Where(x => x.Historico.Equals(item.Historico))
                        .Where(x => x.ValorMovimento.Equals(item.ValorMovimento))
                        .Any();

                    if (!movimento)
                    {
                        context.Movimento.Add(new Movimento
                        {
                            AplicacaoId = AplicacaoId,
                            ContaId = dto.ContaId,
                            TipoConta = dto.TipoConta,
                            DataMovimento = item.DataMovimento,
                            ValorMovimento = item.ValorMovimento,
                            Historico = item.Historico,
                            TipoTransacao = item.TipoTransacao,
                            MovimentoImportacao = movimentoImportacao,
                        });
                    }
                }

                await context.SaveChangesAsync();
                dbContextTransaction.Commit();

                result = new MovimentoImportacaoDetailViewDTO
                        (
                            movimentoImportacao.MovimentoImportacaoId,
                            movimentoImportacao.ImportadoEm
                        );
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw ex;
            }
            finally
            {
                dbContextTransaction.Dispose();
                context.Dispose();
            }

            return result;
        }

        public async Task<MovimentoImportacaoOFXDTO> Preview(byte[] buffer)
        {
            MovimentoImportacaoOFXDTO result = null;

            try
            {
                using (var context = new RThomazDbContext())
                {
                    var stream = new MemoryStream(buffer);
                    var document = new OfxDocument(stream);

                    var banco = await context.Banco
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.CodigoImportacaoOfx.Equals(document.BankID.Trim()))
                        .FirstOrDefaultAsync();

                    if (banco == null)
                    {
                        throw new Exception("Banco não encontrado");
                    }

                    var numDigitosAgencia = 4;
                    var numDigitosConta = 5;

                    var numeroAgencia = document.AccountID.Substring(0, numDigitosAgencia);
                    var numeroConta = document.AccountID.Substring(numDigitosAgencia, numDigitosConta);//Retirei o digito

                    var conta = await context.Conta
                        .OfType<ContaCorrente>()
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.DadoBancario.NumeroAgencia.Contains(numeroAgencia))
                        .Where(x => x.DadoBancario.NumeroConta.Contains(numeroConta))
                        .FirstOrDefaultAsync();

                    if (conta == null)
                    {
                        throw new Exception("Conta não encontrada");
                    }

                    var dataInicioYear = Convert.ToInt32(document.StartDate.Substring(0, 4));
                    var dataInicioMonth = Convert.ToInt32(document.StartDate.Substring(4, 2));
                    var dataInicioDay = Convert.ToInt32(document.StartDate.Substring(6, 2));
                    var dataInicio = new DateTime(dataInicioYear, dataInicioMonth, dataInicioDay);

                    var dataFimYear = Convert.ToInt32(document.EndDate.Substring(0, 4));
                    var dataFimMonth = Convert.ToInt32(document.EndDate.Substring(4, 2));
                    var dataFimDay = Convert.ToInt32(document.EndDate.Substring(6, 2));
                    var dataFim = new DateTime(dataFimYear, dataFimMonth, dataFimDay);

                    var movimentacoes = new List<MovimentoImportacaoOFXItemDTO>();

                    var movimentacoesDB = await context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.DataMovimento >= dataInicio)
                        .Where(x => x.DataMovimento <= dataFim)
                        .Where(x => x.ContaId.Equals(conta.ContaId))
                        .Where(x => x.TipoConta == conta.TipoConta)
                        .ToListAsync();

                    foreach (var item in document.Transactions)
                    {
                        var dataYear = Convert.ToInt32(item.DatePosted.Substring(0, 4));
                        var dataMonth = Convert.ToInt32(item.DatePosted.Substring(4, 2));
                        var dataDay = Convert.ToInt32(item.DatePosted.Substring(6, 2));
                        var data = new DateTime(dataYear, dataMonth, dataDay);

                        var valor = Convert.ToDecimal(item.TransAmount.Replace(",", "").Replace('.', ','));

                        var existe = movimentacoesDB
                                        .Where(x => x.DataMovimento.Equals(data))
                                        .Where(x => x.Historico.Equals(item.Memo))
                                        .Where(x => x.ValorMovimento.Equals(valor))
                                        .Any();

                        movimentacoes.Add(new MovimentoImportacaoOFXItemDTO
                        (
                            tipoTransacao: item.TipoTransacao,
                            dataMovimento: data,
                            valorMovimento: valor,
                            historico: item.Memo,
                            existe: existe
                        ));
                    }

                    result = new MovimentoImportacaoOFXDTO
                    (
                        contaId: conta.ContaId,
                        tipoConta: conta.TipoConta,
                        contaNome: string.Format("Agência: {0}  Conta: {1}", conta.DadoBancario.NumeroAgencia, conta.DadoBancario.NumeroConta),
                        bancoNome: string.Format("{0} ({1})", banco.NomeAbreviado, banco.Numero),
                        dataInicio: dataInicio,
                        dataFim: dataFim,
                        movimentacoes: movimentacoes
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }        

        #endregion
    }
}
