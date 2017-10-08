namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    using System.Collections.Generic;

    public class MasterListModelResponse<TMasterModel>
    {
        #region Fields

        private readonly List<TMasterModel> _data;
        private readonly int _totalRecords;

        #endregion Fields

        #region Constructors

        public MasterListModelResponse(int totalRecords, List<TMasterModel> data)
        {
            _totalRecords = totalRecords;
            _data = data;
        }

        #endregion Constructors

        #region Properties

        public List<TMasterModel> Data
        {
            get { return _data; }
        }

        public int TotalRecords
        {
            get { return _totalRecords; }
        }

        #endregion Properties
    }
}