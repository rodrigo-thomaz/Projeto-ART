using ART.Infra.CrossCutting.WebApi.MasterListDTO;
using System;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListFilterColumnConvertAction : EventArgs
    {
        private readonly MasterListFilterColumn _column;

        public MasterListFilterColumnConvertAction(MasterListFilterColumn column)
        {
            _column = column;
        }

        public MasterListFilterColumn Column { get { return _column; } }
        public IMasterListFilterColumn FilterColumn { get; set; }
    }
}
