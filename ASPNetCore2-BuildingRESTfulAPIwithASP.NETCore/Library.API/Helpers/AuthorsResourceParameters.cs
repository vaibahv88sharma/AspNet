namespace Library.API.Helpers
{
    public class AuthorsResourceParameters
    {
        const int maxPageSize = 20; //  Default Page Size if not provided by User Request
        public int PageNumber { get; set; } = 1;    //  Default Page Number if not provided by User Request

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string Genre { get; set; }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; } = "Name";

        public string Fields { get; set; }
    }
}
