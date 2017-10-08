namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListFilterCriteria
    {
        #region Properties

        public MasterListFilterCondition FilterCondition
        {
            get; set;
        }

        public string Search
        {
            get; set;
        }

        #endregion Properties
    }
}