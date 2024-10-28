namespace DogsHouse.Services.DataPresentation
{
    public class DogsPagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public DogsPagination()
        {
            SetPagination();
        }
        public DogsPagination(int page, int pageSize)
        {
            SetPagination(page, pageSize);
        }
        public void SetPagination(int page = 1, int pageSize = 2)
        {
            Page = page;
            PageSize = pageSize;
        }
        public int GetSkip()
        {
            return (Page * PageSize) - PageSize;
        }
        public int GetTake()
        {
            return PageSize;
        }
    }
}
