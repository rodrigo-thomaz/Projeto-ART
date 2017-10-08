namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTOFilterCriteria
    {
        private readonly string _search;
        private readonly MasterListDTOFilterCondition _filterCondition;

        public MasterListDTOFilterCriteria(string search, MasterListDTOFilterCondition filterCondition)
        {
            _search = search;
            _filterCondition = filterCondition;
        }

        public string Search { get { return _search; } }
        public MasterListDTOFilterCondition FilterCondition { get { return _filterCondition; } }
    }
}
