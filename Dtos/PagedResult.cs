namespace QLBaoDienTu.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = GetTotalPaging(pageNumber, pageSize, count);
        }

        public int GetTotalPaging(int pageNumber, int pageSize, int totalRecords)
        {
            var totalPages = Convert.ToDouble(totalRecords / pageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            return roundedTotalPages;
        }
    }
}
