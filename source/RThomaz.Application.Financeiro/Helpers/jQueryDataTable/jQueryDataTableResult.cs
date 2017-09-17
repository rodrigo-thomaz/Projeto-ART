namespace RThomaz.Application.Financeiro.Helpers.jQueryDataTable
{
    public class jQueryDataTableResult<TData>
    {
        private readonly int _echo;
        private readonly int _totalRecords;
        private readonly int _totalDisplayRecords;
        private readonly TData _data;

        public jQueryDataTableResult(int echo, int totalRecords, int totalDisplayRecords, TData data)
        {
            _echo = echo;
            _totalRecords = totalRecords;
            _totalDisplayRecords = totalDisplayRecords;
            _data = data;
        }

        public int sEcho { get { return _echo; } }
        public int iTotalRecords { get { return _totalRecords; } }
        public int iTotalDisplayRecords { get { return _totalDisplayRecords; } }
        public TData aaData { get { return _data; } }
    }
}