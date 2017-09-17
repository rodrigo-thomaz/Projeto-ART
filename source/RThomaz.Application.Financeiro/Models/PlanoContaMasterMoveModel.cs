namespace RThomaz.Application.Financeiro.Models
{
    public class PlanoContaMasterMoveModel
    {
        public long[] PlanoContaIds { get; set; }

        public long? ParentId { get; set; }
    }
}