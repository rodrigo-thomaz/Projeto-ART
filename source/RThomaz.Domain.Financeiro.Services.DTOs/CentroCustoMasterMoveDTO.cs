namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CentroCustoMasterMoveDTO
    {
        private readonly long[] _centroCustoIds;
        private readonly long? _parentId;

        public CentroCustoMasterMoveDTO(long[] centroCustoIds, long? parentId)
        {
            _centroCustoIds = centroCustoIds;
            _parentId = parentId;
        }

        public long[] CentroCustoIds { get { return _centroCustoIds; } }

        public long? ParentId { get { return _parentId; } }
    }
}
