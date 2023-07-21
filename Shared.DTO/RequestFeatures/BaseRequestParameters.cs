namespace Shared.DTO.RequestFeatures
{
    public abstract class BaseRequestParameters
    {
        public const int MaxPageSize = 50;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int CurrentPage { get; init; } = 1;

        public string? SearchTerm { get; init; }
    }
}
