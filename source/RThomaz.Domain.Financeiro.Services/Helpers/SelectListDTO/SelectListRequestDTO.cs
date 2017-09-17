namespace RThomaz.Business.Helpers.SelectListDTO
{
    public class SelectListDTORequest
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;
        private readonly string _search;

        public SelectListDTORequest(int pageNumber, int pageSize, string search)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            _search = search;
        }

        public int Skip
        {
            get
            {
                return (_pageNumber - 1) * _pageSize;
            }
        }

        public int PageNumber { get { return _pageNumber; } }
        public int PageSize { get { return _pageSize; } }
        public string Search { get { return _search; } }
    }
}
