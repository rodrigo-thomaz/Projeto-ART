namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListFilterColumn : IMasterListFilterColumn
    {
        #region Properties

        public string ColumnName
        {
            get; set;
        }

        public MasterListFilterCriteria[] Criteria
        {
            get; set;
        }

        #endregion Properties
    }
}