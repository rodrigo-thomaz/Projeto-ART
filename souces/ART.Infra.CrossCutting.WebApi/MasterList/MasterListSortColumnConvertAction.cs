using ART.Infra.CrossCutting.WebApi.MasterListDTO;
using System;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListSortColumnConvertAction : EventArgs
    {
        private readonly IMasterListSortColumn _column;

        public MasterListSortColumnConvertAction(IMasterListSortColumn column)
        {
            _column = column;
        }

        public IMasterListSortColumn Column { get { return _column; } }
        public IMasterListSortColumn SortColumn { get; set; }
    }
}
