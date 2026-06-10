namespace QLBaoDienTu.Dtos
{
    public class Paging
    {
        public int TotalPages { get; }
        public int TotalRecords { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        private Paging(int pageNumber, int pageSize, int totalPages, int totalRecords)
        {
            this.TotalPages = totalPages;
            this.TotalRecords = totalRecords;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public static Paging GetPaging(int pageNumber, int pageSize, int totalRecords)
        {
            var totalPages = Convert.ToDouble(totalRecords / pageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            return new Paging(pageNumber, pageSize, roundedTotalPages, totalRecords);
        }

        public static Paging DefaultPaging() => new Paging(0, 0, 0, 0);
    }
}
