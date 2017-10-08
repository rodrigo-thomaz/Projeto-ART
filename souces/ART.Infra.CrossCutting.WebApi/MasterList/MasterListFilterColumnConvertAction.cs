using ART.Infra.CrossCutting.WebApi.MasterListDTO;
using System;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListFilterColumnConvertAction : EventArgs
    {
        private readonly IMasterListFilterColumn _column;

        public MasterListFilterColumnConvertAction(IMasterListFilterColumn column)
        {
            _column = column;
        }

        public IMasterListFilterColumn Column { get { return _column; } }
        public IMasterListFilterColumn FilterColumn { get; set; }
    }
}
