namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public interface IMasterListSortColumn
    {
        #region Properties

        string ColumnName
        {
            get;
        }

        int Priority
        {
            get;
        }

        MasterListSortDirection SortDirection
        {
            get;
        }

        #endregion Properties
    }
}