namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public interface IMasterListFilterColumn
    {
        #region Properties

        string ColumnName
        {
            get;
        }

        MasterListFilterCriteria[] Criteria
        {
            get;
        }

        #endregion Properties
    }
}