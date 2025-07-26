using System.ComponentModel.DataAnnotations;

namespace HelloToAsp.Dtos.Auth
{
    public class RegUserDto : LogUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
