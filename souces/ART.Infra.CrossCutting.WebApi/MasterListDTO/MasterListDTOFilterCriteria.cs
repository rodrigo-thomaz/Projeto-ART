using ART.Infra.CrossCutting.WebApi.MasterList;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTOFilterCriteria
    {
        private readonly string _search;
        private readonly MasterListFilterCondition _filterCondition;

        public MasterListDTOFilterCriteria(string search, MasterListFilterCondition filterCondition)
        {
            _search = search;
            _filterCondition = filterCondition;
        }

        public string Search { get { return _search; } }
        public MasterListFilterCondition FilterCondition { get { return _filterCondition; } }
    }
}
