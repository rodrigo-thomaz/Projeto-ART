namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    using System;

    public class MasterListSortColumnConvertAction : EventArgs
    {
        #region Fields

        private readonly IMasterListSortColumn _column;

        #endregion Fields

        #region Constructors

        public MasterListSortColumnConvertAction(IMasterListSortColumn column)
        {
            _column = column;
        }

        #endregion Constructors

        #region Properties

        public IMasterListSortColumn Column
        {
            get { return _column; }
        }

        public IMasterListSortColumn SortColumn
        {
            get; set;
        }

        #endregion Properties
    }
}