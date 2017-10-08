namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListFilterColumn : IMasterListFilterColumn
    {
        public string ColumnName { get; set; }

        public MasterListFilterCriteria[] Criteria { get; set; }
    }
}
