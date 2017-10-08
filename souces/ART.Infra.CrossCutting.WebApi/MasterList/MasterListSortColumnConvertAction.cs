using ART.Infra.CrossCutting.WebApi.MasterListDTO;
using System;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListSortColumnConvertAction : EventArgs
    {
        private readonly MasterListSortColumn _column;

        public MasterListSortColumnConvertAction(MasterListSortColumn column)
        {
            _column = column;
        }

        public MasterListSortColumn Column { get { return _column; } }
        public IMasterListSortColumn SortColumn { get; set; }
    }
}
