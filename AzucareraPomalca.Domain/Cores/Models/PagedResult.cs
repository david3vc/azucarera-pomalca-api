namespace AzucareraPomalca.Domain.Cores.Models
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Data { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public int Total { get; set; }

        public PagedResult(IReadOnlyList<T> data, Paging paging, int totalElements)
        {
            Data = data;

            // Process start
            int total = totalElements;
            int page = paging.PageNumber;
            int perPage = paging.PageSize;

            int lastPage = (int)Math.Ceiling((decimal)total / perPage);

            if (lastPage < 1) lastPage = 1;

            int currentPage = page;

            if (page > lastPage) page = lastPage;

            int to = page * perPage;
            if (to > total) to = total;

            int from = (page - 1) * perPage + 1;
            if (total <= 0) from = 0;
            // Process end

            From = from;
            To = to;
            PerPage = perPage;
            CurrentPage = currentPage;
            LastPage = lastPage;
            Total = total;
        }
    }
}
