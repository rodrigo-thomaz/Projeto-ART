using ART.Infra.CrossCutting.WebApi.MasterList;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public interface IMasterListDTOSortColumn
    {
        string ColumnName { get; }
        MasterListSortDirection SortDirection { get; }
    }
}
