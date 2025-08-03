namespace HelloToAsp.Dtos
{
    public class PaginationRequestDto
    {
        public int Page { get; set; }
        public int Size { get; set; } = 10;
    }
}
