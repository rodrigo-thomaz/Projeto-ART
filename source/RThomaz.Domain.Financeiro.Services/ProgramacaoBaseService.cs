using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public abstract class ProgramacaoBaseService : ServiceBase
    {
        #region constructors

        public ProgramacaoBaseService()
        {

        }

        #endregion

        protected List<DateTime> GetListOfDate(ProgramacaoDetailBaseDTO programacaoContractBase)
        {
            List<DateTime> result = new List<DateTime>();
            DateTime currentDate = programacaoContractBase.DataInicial;

            while (currentDate <= programacaoContractBase.DataFinal)
            {
                switch (programacaoContractBase.Frequencia)
                {
                    case Frequencia.Diariamente:
                        {
                            result.Add(currentDate);
                        }
                        break;
                    case Frequencia.Semanal:
                        {
                            if ((programacaoContractBase.HasDomingo.HasValue && programacaoContractBase.HasDomingo.Value && currentDate.DayOfWeek == DayOfWeek.Sunday)
                                    || (programacaoContractBase.HasSegunda.HasValue && programacaoContractBase.HasSegunda.Value && currentDate.DayOfWeek == DayOfWeek.Monday)
                                    || (programacaoContractBase.HasTerca.HasValue && programacaoContractBase.HasTerca.Value && currentDate.DayOfWeek == DayOfWeek.Tuesday)
                                    || (programacaoContractBase.HasQuarta.HasValue && programacaoContractBase.HasQuarta.Value && currentDate.DayOfWeek == DayOfWeek.Wednesday)
                                    || (programacaoContractBase.HasQuinta.HasValue && programacaoContractBase.HasQuinta.Value && currentDate.DayOfWeek == DayOfWeek.Thursday)
                                    || (programacaoContractBase.HasSexta.HasValue && programacaoContractBase.HasSexta.Value && currentDate.DayOfWeek == DayOfWeek.Friday)
                                    || (programacaoContractBase.HasSabado.HasValue && programacaoContractBase.HasSabado.Value && currentDate.DayOfWeek == DayOfWeek.Saturday)
                                )
                            {
                                result.Add(currentDate);
                            }
                        }
                        break;
                    case Frequencia.Mensal:
                    case Frequencia.Bimestral:
                    case Frequencia.Trimestral:
                    case Frequencia.Quadrimestral:
                    case Frequencia.Semestral:
                    case Frequencia.Anual:
                        {
                            int step = 0;

                            if (programacaoContractBase.Frequencia == Frequencia.Mensal) step = 1;
                            if (programacaoContractBase.Frequencia == Frequencia.Bimestral) step = 2;
                            if (programacaoContractBase.Frequencia == Frequencia.Trimestral) step = 3;
                            if (programacaoContractBase.Frequencia == Frequencia.Quadrimestral) step = 4;
                            if (programacaoContractBase.Frequencia == Frequencia.Semestral) step = 6;
                            if (programacaoContractBase.Frequencia == Frequencia.Anual) step = 12;

                            var lastDayOfMonth = new DateTime(
                                        currentDate.Month == 12 ? currentDate.Year + 1 : currentDate.Year
                                    , currentDate.Month == 12 ? 1 : currentDate.Month + 1, 1).AddDays(-1);

                            if ((currentDate.Day == programacaoContractBase.Dia) || (currentDate.Day == lastDayOfMonth.Day && programacaoContractBase.Dia >= lastDayOfMonth.Day))
                            {
                                result.Add(currentDate);
                                currentDate = currentDate.AddDays(-5);
                                currentDate = currentDate.AddMonths(step);
                            }
                        }
                        break;
                    default:
                        break;
                }
                currentDate = currentDate.AddDays(1);
            }

            return result;
        }
    }
}
