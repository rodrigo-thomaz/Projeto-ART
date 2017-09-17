namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers
{
    public class MesAnoDTO
    {
        private readonly int _mes;
        private readonly int _ano;

        public MesAnoDTO
            (
                  int mes
                , int ano
            )
        {
            _mes = mes;
            _ano = ano;
        }

        public int Mes { get { return _mes; } }
        public int Ano { get { return _ano; } }
    }
}
