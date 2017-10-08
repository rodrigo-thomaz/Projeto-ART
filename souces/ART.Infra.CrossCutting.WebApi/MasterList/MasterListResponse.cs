using System.Collections.Generic;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListModelResponse<TMasterModel>
    {
        #region private fields

        private readonly int _totalRecords;
        private readonly List<TMasterModel> _data;

        #endregion

        #region constructors

        public MasterListModelResponse(int totalRecords, List<TMasterModel> data)
        {
            _totalRecords = totalRecords;
            _data = data;
        } 

        #endregion

        #region public properties

        public int TotalRecords { get { return _totalRecords; } }
        public List<TMasterModel> Data { get { return _data; } }
        
        #endregion        
    }
}
