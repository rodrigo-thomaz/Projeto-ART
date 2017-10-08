namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListSortColumn : IMasterListSortColumn
    {
        #region Properties

        public string ColumnName
        {
            get; set;
        }

        public int Priority
        {
            get; set;
        }

        public MasterListSortDirection SortDirection
        {
            get; set;
        }

        #endregion Properties
    }
}