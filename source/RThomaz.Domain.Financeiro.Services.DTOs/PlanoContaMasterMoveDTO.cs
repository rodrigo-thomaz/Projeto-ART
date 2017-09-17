namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PlanoContaMasterMoveDTO
    {
        private readonly long[] _planoContaIds;
        private readonly long? _parentId;

        public PlanoContaMasterMoveDTO(long[] planoContaIds, long? parentId)
        {
            _planoContaIds = planoContaIds;
            _parentId = parentId;
        }

        public long[] PlanoContaIds { get { return _planoContaIds; } }

        public long? ParentId { get { return _parentId; } }
    }
}
