using ART.Infra.CrossCutting.WebApi.MasterList;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public interface IMasterListDTOFilterColumn
    {
        string ColumnName { get; }

        MasterListFilterCriteria[] Criteria { get; }
    }
}
