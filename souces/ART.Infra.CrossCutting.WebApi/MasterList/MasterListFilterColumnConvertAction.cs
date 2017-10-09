namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    using System;

    public class MasterListFilterColumnConvertAction : EventArgs
    {
        #region Fields

        private readonly IMasterListFilterColumn _column;

        #endregion Fields

        #region Constructors

        public MasterListFilterColumnConvertAction(IMasterListFilterColumn column)
        {
            _column = column;
        }

        #endregion Constructors

        #region Properties

        public IMasterListFilterColumn Column
        {
            get { return _column; }
        }

        public IMasterListFilterColumn FilterColumn
        {
            get; set;
        }

        #endregion Properties
    }
}