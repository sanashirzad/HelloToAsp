namespace HelloToAsp.Core.Dtos
{
    public class PaginationResponseDto<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public int Size { get; set; }
        public List<T> Records { get; set; }
    }
}
