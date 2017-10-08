namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public interface IMasterListDTOSortColumn
    {
        string ColumnName { get; }
        MasterListDTOSortDirection SortDirection { get; }
    }
}
