namespace src.Utils
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
