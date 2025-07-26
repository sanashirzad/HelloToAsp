namespace HelloToAsp.Dtos.User
{
    public class UpdateDto : BaseDto
    {
        public int Id { get; set; }
        public string UserName => PhoneNumber;
        public string NormalizedUserName => PhoneNumber;
    }
}
