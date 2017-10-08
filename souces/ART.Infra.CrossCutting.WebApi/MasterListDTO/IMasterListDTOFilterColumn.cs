namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public interface IMasterListDTOFilterColumn
    {
        string ColumnName { get; }

        MasterListDTOFilterCriteria[] Criteria { get; }
    }
}
