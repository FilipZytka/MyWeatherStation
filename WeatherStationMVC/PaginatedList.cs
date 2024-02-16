using System.Drawing.Printing;

namespace WeatherStationMVC
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }

        public PaginatedList(List<T> list, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(list);
        }
        public static PaginatedList<T> Create(List<T> list, int pageIndex, int pageSize)
        {
            var count = list.Count;
            var items = list.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);

        }


    }
}
