using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.DTO
{
    public class PagedSearchDTO<T> where T : class
    {
        public int CurrentPage { get; set; }
        public List<T> List { get; set; }
        public int PageSize { get; set; }
        public string SortDirection { get; set; }
        public int TotalResults { get; set; }
    }
}
