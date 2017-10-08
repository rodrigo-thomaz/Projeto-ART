using ART.Infra.CrossCutting.WebApi.MasterList;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public interface IMasterListFilterColumn
    {
        string ColumnName { get; }

        MasterListFilterCriteria[] Criteria { get; }
    }
}
