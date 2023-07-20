namespace Shared.DTO.RequestFeatures.Paging
{
    public abstract class BasePagingRequestParameters
    {
        public const int MaxPageSize = 50;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int CurrentPage { get; set; } = 1;
    }
}
